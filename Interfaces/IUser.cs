using BlogocomApiV2.Models;
using System.Threading.Tasks;

namespace BlogocomApiV2.Interfaces
{
    public interface IUser
    {
        User? FindOfDefaultUserByPhone(string phone);
        Task<User> AddUserAsync(User user);
    }
}
