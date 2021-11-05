using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Chats
{
    [ExtendObjectType(Name = "Query")]
    public class ChatQueries
    {
        //[UsePaging]
        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Get chats.")]
        public IQueryable<Chat> GetChats (
            [ScopedService] ApiDbContext DB,
            [Service] UserService _userService,
            [Service] IChat _chatRepository
            )
        {
            long userId = _userService.GetUserId();
            IEnumerable<long> ids = DB.UserChats.Where(i => i.UserId == userId).Select(c => c.ChatId).ToArray();
            return DB.Chats.AsQueryable().Where(d => ids.Contains(d.Id));
        }
    }
}
