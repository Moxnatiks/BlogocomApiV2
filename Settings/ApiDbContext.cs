using BlogocomApiV2.Models;
using BlogocomApiV2.Models.Configs;
using Microsoft.EntityFrameworkCore;

namespace BlogocomApiV2.Settings
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ChatConfig());
            modelBuilder.ApplyConfiguration(new UserChatConfig());
            modelBuilder.ApplyConfiguration(new MessageConfig());
            modelBuilder.ApplyConfiguration(new UserAvatarConfig());


            base.OnModelCreating(modelBuilder);
        }
    }
}
