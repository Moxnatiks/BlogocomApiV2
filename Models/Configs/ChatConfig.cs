using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogocomApiV2.Models.Configs
{
    public class ChatConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(i => i.IsPrivate).HasDefaultValue(true);

            builder
                .HasMany(g => g.UserChats)
                .WithOne(u => u.Chat)
                .HasForeignKey(k => k.ChatId);

            builder
                .HasMany(b => b.Messages)
                .WithOne(s => s.Chat)
                .HasForeignKey(s => s.ChatId);

            //Seeds
            /*var Ids = 1;
            var chatFaker = new Faker<Chat>()

                .RuleFor(o => o.Id, f => Ids++)
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.UserId, f => f.Random.Int(1, 10))
                .RuleFor(o => o.CreatedDate, DateTimeOffset.Now);

            IList<Chat> chats = chatFaker.Generate(7);

            builder.HasData(chats)*/;
        }
    }
}
