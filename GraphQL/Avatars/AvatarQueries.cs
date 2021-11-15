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

namespace BlogocomApiV2.GraphQL.Avatars
{
    [ExtendObjectType(Name = "Query")]
    public class AvatarQueries
    {
        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Get last avatars by user Id")]
        public Models.File GetAvatars (
            long userId,
            [ScopedService] ApiDbContext DB
            )
        {

            /*long userId = _userService.GetUserId();
            IEnumerable<long> ids = DB.UserChats.Where(i => i.UserId == userId).Select(c => c.ChatId).ToArray();
            return DB.Chats.AsQueryable().Where(d => ids.Contains(d.Id));*/

            IEnumerable<long> ids = DB.UserAvatars.Where(i => i.UserId == userId).Select(d => d.UserId).ToArray();
            return DB.Files.AsQueryable().Where(i => ids.Contains(i.Id)).OrderByDescending(d => d.CreatedDate).FirstOrDefault();
        }

    }
}
