using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Chats
{
    public class ChatType : ObjectType<Chat>
    {
        protected override void Configure(IObjectTypeDescriptor<Chat> descriptor)
        {
            descriptor.Description("Represents any executable chat.");


            descriptor
                .Field(c => c.Messages)
                .Name("messages")
                .ResolveWith<Resolvers>(c => c.GetMessages(default!, default!))
                .UseDbContext<ApiDbContext>()
                .Description("Last 100 sms of chat.");


        }

        private class Resolvers
        {
            public IQueryable<Message> GetMessages(Chat chat, [ScopedService] ApiDbContext DB)
            {
                return DB.Messages.Where(i => i.ChatId == chat.Id).OrderByDescending(d => d.CreatedDate).Take(100);
            }

        }
    }
}
