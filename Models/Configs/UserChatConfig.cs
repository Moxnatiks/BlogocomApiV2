using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models.Configs
{
    public class UserChatConfig : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            //Relations
            builder.HasKey(bc => new { bc.ChatId, bc.UserId });

            builder
                .HasOne(bc => bc.Chat)
                .WithMany(b => b.UserChats)
                .HasForeignKey(bc => bc.ChatId);

            builder
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserChats)
                .HasForeignKey(bc => bc.UserId);



            //Rule
            //builder.Property(t => t.Status).HasMaxLength(32);

            // Seeds
            /*var status = new[] { "admin", "guest", "vasia", "dog", "kiwi" };

            var Ids = 1;
            var groupeFaker = new Faker<UserChat>()

                .RuleFor(o => o.UserId, f => f.Random.Int(1, 3))
                .RuleFor(o => o.ChatId, f => Ids++)
                .RuleFor(o => o.Status, f => f.PickRandom(status))
                .RuleFor(o => o.DateAdded, DateTimeOffset.Now);

            IList<UserChat> groups = groupeFaker.Generate(7);

            builder.HasData(groups);*/
        }
    }
}
