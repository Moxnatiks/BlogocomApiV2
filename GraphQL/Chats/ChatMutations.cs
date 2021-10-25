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

namespace BlogocomApiV2.GraphQL.Chats
{
    [ExtendObjectType(Name = "Mutation")]
    public class ChatMutations
    {
        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Create private chat")]
        public async Task<Chat> CreatePrivateChatAsync(
                AddPrivateChatInput input,
                [ScopedService] ApiDbContext DB,
                [Service] ITopicEventSender eventSender,
                [Service] UserService _userService,
                CancellationToken cancellationToken)
        {
            long userId = _userService.GetUserId();

            User user1 = DB.Users.FirstOrDefault(u => u.Id == input.RecipientId);
            User user2 = DB.Users.FirstOrDefault(u => u.Id == userId);

            if (user1 == null || user2 == null) throw new ArgumentException("Error!");
            
            long[] ids1 = DB.UserChats.Where(u => u.UserId == input.RecipientId).Select(i => i.ChatId).ToArray();
            long[] ids2 = DB.UserChats.Where(u => u.UserId == userId).Select(i => i.ChatId).ToArray();

            long[] chatIds = ids1.Intersect(ids2).ToArray();

            if (chatIds.Length > 0)
            {
                Chat chat = DB.Chats.FirstOrDefault(c => c.Id == chatIds[0]);
                return chat;
            } else {
                Chat newChat = new Chat
                {
                    UserId = userId,
                };

                DB.Chats.Add(newChat);
                DB.SaveChanges();

                List<UserChat> userChats = new List<UserChat> {
                    new UserChat{
                        ChatId = newChat.Id,
                        UserId = input.RecipientId
                    },
                    new UserChat
                    {
                        ChatId = newChat.Id,
                        UserId = userId
                     }};

                DB.UserChats.AddRange(userChats);
                await DB.SaveChangesAsync();

                await eventSender.SendAsync(
                   "OnCreatedChat_" + input.RecipientId,
                   newChat, cancellationToken);
                return newChat;
            }
        }
    }
}
