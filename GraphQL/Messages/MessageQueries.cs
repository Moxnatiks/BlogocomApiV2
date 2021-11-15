using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Linq;

namespace BlogocomApiV2.GraphQL.Messages
{
    [ExtendObjectType(Name = "Query")]
    public class MessageQueries
    {
        //[UsePaging]
        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Get messages by chatId")]
        public IQueryable<Message> GetMessages(
            long chatId,
            [ScopedService] ApiDbContext DB,
            [Service] UserService _userService,
            [Service] IChat _chatRepository )
        {
            if (_chatRepository.CheckUserAccessToChat(_userService.GetUserId(), chatId))
            {
                return DB.Messages.Where(c => c.ChatId == chatId).OrderByDescending(c => c.CreatedDate);
            }
            else throw new ArgumentException("NO access!!!");
        }

        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Get message by id.")]
        public Message? GetMessageById (
            long messageId,
            [ScopedService] ApiDbContext DB)
        {
            return DB.Messages.FirstOrDefault(i => i.Id == messageId);
        }

        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Get message by ids")]
        public IQueryable<Message> GetMessageByIds(
            long [] messageIds,
            [ScopedService] ApiDbContext DB)
        {
            return DB.Messages.Where(a => messageIds.Contains(a.Id));
        }
    }
}
