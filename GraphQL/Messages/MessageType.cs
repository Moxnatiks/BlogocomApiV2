using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Messages
{
    public class MessageType : ObjectType<Message>
    {
        protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
        {
            descriptor.Description("Represents any executable message.");

            descriptor.Field(i => i.Id).Description("Represents the unique ID for the message.");

            descriptor
                .Field(c => c.Chat)
                .Name("chat")
                .ResolveWith<Resolvers>(c => c.GetChat(default!, default!))
                .UseDbContext<ApiDbContext>()
                .Description("Get chat.");

            descriptor
               .Field(c => c.User)
               .Name("user")
               .ResolveWith<Resolvers>(c => c.GetUser(default!, default!))
               .UseDbContext<ApiDbContext>()
               .Description("Get user.");

            descriptor
                .Field("files")
                .Name("files")
                .ResolveWith<Resolvers>(c => c.GetFiles(default!, default!))
                .UseDbContext<ApiDbContext>()
                .Description("Get files.");
        }

        private class Resolvers
        {
            public Chat GetChat(Message message, [ScopedService] ApiDbContext DB)
            {
                return DB.Chats.FirstOrDefault(i => i.Id == message.ChatId);
            }

            public User GetUser(Message message, [ScopedService] ApiDbContext DB)
            {
                return DB.Users.FirstOrDefault(i => i.Id == message.UserId);
            }

            public IQueryable<File> GetFiles (Message message, [ScopedService] ApiDbContext DB)
            {

                IEnumerable<long> ids = DB.MessageFiles.Where(c => c.MessageId == message.Id).Select(r => r.FileId).ToArray();
                return DB.Files.AsQueryable().Where(a => ids.Contains(a.Id));

            }
        }
    }
}
