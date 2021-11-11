using BlogocomApiV2.Interfaces;
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
        [GraphQLDescription("Create message.")]
        public Message CreateMessage(
        AddMessageInput input,
        [ScopedService] ApiDbContext DB,
        [Service] ITopicEventSender eventSender,
        [Service] UserService _userService,
        [Service] IChat _chatRepository,
        [Service] IMessage _messageRepository,
        CancellationToken cancellationToken)
        {
            long userId = _userService.GetUserId();

            if (!_chatRepository.CheckUserAccessToChat(_userService.GetUserId(), input.ChatId)) throw new ArgumentException("NO access!");

            Message newMessage = new Message
            {
                ChatId = input.ChatId,
                Content = input.Content,
                UserId = userId
            };

            //newMessage = await _messageRepository.CreateMessageAsync(newMessage);

            DB.Messages.Add(newMessage);
            DB.SaveChanges();

            if (input.fileIds.Length > 0)
            {
                List<MessageFile> messagefiles = new List<MessageFile>();

                foreach (long el in input.fileIds)
                {

                    messagefiles.Add(new MessageFile
                    {
                        MessageId = newMessage.Id,
                        FileId = el
                    });
                }

                DB.MessageFiles.AddRange(messagefiles);
                DB.SaveChanges();
            }


            /*long[] idRecipients = DB.UserChats.Where(c => c.ChatId == input.ChatId).Select(u => u.UserId).ToArray();

            foreach (long el in idRecipients)
            {
                if (el == userId) continue;

                eventSender.SendAsync(
                    "OnCreatedMessage_" + el,
                    newMessage, cancellationToken);
            }*/

            return newMessage;
        }

        /*        [Authorize]
                [UseDbContext(typeof(ApiDbContext))]
                [GraphQLDescription("Create message.")]
                public async Task<Message> CreateMessageAsync(
                        AddMessageInput input,
                        [ScopedService] ApiDbContext DB,
                        [Service] ITopicEventSender eventSender,
                        [Service] UserService _userService,
                        [Service] IChat _chatRepository,
                        [Service] IMessage _messageRepository,
                        CancellationToken cancellationToken)
                {
                    long userId = _userService.GetUserId();

                    if (!_chatRepository.CheckUserAccessToChat(_userService.GetUserId(), input.ChatId)) throw new ArgumentException("NO access!");

                    Message newMessage = new Message
                    {
                        ChatId = input.ChatId,
                        Content = input.Content,
                        UserId = userId
                    };

                    //newMessage = await _messageRepository.CreateMessageAsync(newMessage);

                    newMessage = DB.Messages.Add(newMessage);
                    DB.SaveChanges();

                    if (input.fileIds.Length > 0)
                    {
                        List<MessageFile> messagefiles = new List<MessageFile>();

                        foreach (long el in input.fileIds)
                        {

                            messagefiles.Add(new MessageFile
                            {
                                MessageId = newMessage.Id,
                                FileId = el
                            });
                        }

                        DB.MessageFiles.AddRange(messagefiles);
                        await DB.SaveChangesAsync();
                    } 


                    long[] idRecipients = DB.UserChats.Where(c => c.ChatId == input.ChatId).Select(u => u.UserId).ToArray();

                    foreach (long el in idRecipients)
                    {
                        if (el == userId) continue;

                        await eventSender.SendAsync(
                            "OnCreatedMessage_" + el,
                            newMessage, cancellationToken);
                    }

                    return newMessage;
                }*/

        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Update message.")]
        public async Task<Message> UpdateMessageAsync (
            UpdateMessageInput input,
            [ScopedService] ApiDbContext DB,
            [Service] UserService _userService,
            [Service] IMessage _messageRepository,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            )
        {
            long userId = _userService.GetUserId();
            if (!_messageRepository.CheckUserAccessToMessage(userId, input.messageId)) throw new ArgumentException("No access!");

            Message existMessage = _messageRepository.GetMessageById(input.messageId);

            existMessage.Content = input.content;

            DB.Messages.Update(existMessage);
            await DB.SaveChangesAsync(cancellationToken);

            long[] userIds = DB.UserChats.Where(r => r.ChatId == existMessage.ChatId).Select(u => u.UserId).ToArray();

            foreach (long el in userIds)
            {
                if (el == userId) continue;

                await eventSender.SendAsync(
                "OnUpdatedMessage_" + el,
                existMessage, cancellationToken);
            }

            return existMessage;
        }
    }
}
