using BlogocomApiV2.Models;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Messages
{
    [ExtendObjectType(Name = "Mutation")]
    public class MessageMutations
    {
        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Create message")]
        public async Task<Message> CreateMessageAsync(
                AddMessageInput input,
                [ScopedService] ApiDbContext DB,
                [Service] ITopicEventSender eventSender,
                [Service] UserService _userService,
                CancellationToken cancellationToken)
        {
            long userId = _userService.GetUserId();
            long[] idsUsers = DB.UserChats.Where(u => u.UserId == userId).Select(i => i.ChatId).ToArray();

            long? index = Array.IndexOf(idsUsers, input.ChatId);
            if (index == null) throw new ArgumentException("Invalid id chat!");

            Message newMessage = new Message
            {
                ChatId = input.ChatId,
                Content = input.Content,
                UserId = userId
            };

            DB.Messages.Add(newMessage);

            await DB.SaveChangesAsync(cancellationToken);

            long[] idRecipients = DB.UserChats.Where(c => c.ChatId == input.ChatId).Select(u => u.UserId).ToArray();

            foreach (long el in idRecipients)
            {
                if (el == userId) continue;

                await eventSender.SendAsync(
                "OnCreatedMessage_" + el,
                newMessage, cancellationToken);
            }

            return newMessage;
        }
    }
}
