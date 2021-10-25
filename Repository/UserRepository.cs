using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApiDbContext DB;
        public UserRepository(ApiDbContext apiDbContext)
        {
            DB = apiDbContext;
        }

        async public Task<User> AddUserAsync(User user)
        {
            DB.Users.AddAsync(user);
            await DB.SaveChangesAsync();
            return user;
        }

        public User? FindOfDefaultUserByPhone(string phone)
        {
            User user =  DB.Users.FirstOrDefault(p => p.Phone == phone);
            return user;
        }
    }
}
