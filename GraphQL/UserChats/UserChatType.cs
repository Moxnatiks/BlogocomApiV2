using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.UserChats
{
 
    public class UserChatType : ObjectType<UserChat>
    {
        protected override void Configure(IObjectTypeDescriptor<UserChat> descriptor)
        {
            descriptor.Description("Represents linking table between chats and users.");

            descriptor
               .Field(p => p.UserId).Ignore();
            descriptor
                .Field(p => p.ChatId).Ignore();


            descriptor
                .Field(c => c.Chat)
                .Name("chat")
                .ResolveWith<Resolvers>(c => c.GetChat(default!, default!))
                .UseDbContext<ApiDbContext>()
                .Description("Chat in which users participate .");

            descriptor
               .Field(c => c.User)
               .Name("user")
               .ResolveWith<Resolvers>(c => c.GetUser(default!, default!))
               .UseDbContext<ApiDbContext>()
               .Description("Users of which users are members .");


        }

        private class Resolvers
        {
            public Chat GetChat(UserChat userChat, [ScopedService] ApiDbContext DB)
            {
                return DB.Chats.FirstOrDefault(i => i.Id == userChat.ChatId);
            }

            public User GetUser(UserChat userChat, [ScopedService] ApiDbContext DB)
            {
                return DB.Users.FirstOrDefault(i => i.Id == userChat.UserId);
            }
        }
    }
}
