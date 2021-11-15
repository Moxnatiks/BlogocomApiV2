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
            return DB.Files.Where(i => i.Id == userId).OrderByDescending(d => d.CreatedDate).FirstOrDefault();
        }

    }
}
