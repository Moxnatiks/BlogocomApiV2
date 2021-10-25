using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BlogocomApiV2.Models.Configs
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(ss => ss.Chat)
                .WithMany(s => s.Messages)
                .HasForeignKey(ss => ss.ChatId);

            //Seeds
            /*var Ids = 1;
            var messageFaker = new Faker<Message>()

                .RuleFor(o => o.Id, f => Ids++)
                .RuleFor(o => o.ChatId, f => f.Random.Int(1, 7))
                .RuleFor(o => o.UserId, f => f.Random.Int(1, 3))
                .RuleFor(o => o.Content, f => f.Lorem.Lines())
                .RuleFor(o => o.CreatedDate, DateTimeOffset.Now);

            IList<Message> messages = messageFaker.Generate(20);

            builder.HasData(messages);*/
        }
    }
}
