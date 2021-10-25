using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogocomApiV2.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Represents any executable user.");

            descriptor
               .Field(p => p.IsAccess).Ignore();
            descriptor
                .Field(p => p.Password).Ignore();
            descriptor
                .Field(p => p.StoredSalt).Ignore();


            descriptor
                .Field(c => c.Id)
                .Description("Represents the unique ID for the user.");

            descriptor
                .Field(c => c.Chats)
                .Name("chats")
                .ResolveWith<Resolvers>(c => c.GetChats(default!, default!))
                .UseDbContext<ApiDbContext>()
                .Description("The chats in which the user.");
        }

        private class Resolvers
        {
            public IQueryable<Chat> GetChats(User user, [ScopedService] ApiDbContext DB)
            {
                IEnumerable<long> ids = DB.UserChats.Where(r => r.UserId == user.Id).Select(r => r.ChatId);

                IQueryable<Chat> chats = DB.Chats.AsQueryable().Where(a => ids.Contains(a.Id));

                return chats;
            }
        }
    }
}
