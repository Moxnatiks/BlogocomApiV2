using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Repository;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace BlogocomApiV2.GraphQL.Users
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        [Authorize]
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Get my data")]
        public User GetUser([ScopedService] ApiDbContext DB,
                            [Service] UserService _userService)
        {
            return DB.Users.FirstOrDefault(x => x.Id == _userService.GetUserId());
        }
    }
    
}
