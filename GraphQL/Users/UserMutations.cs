using BlogocomApiV2.GraphQl.Users;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Users
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        [UseDbContext(typeof(ApiDbContext))]
        [GraphQLDescription("Login user")]
        public async Task<TokenResponse> LoginUserAsync(
            LoginUserInput input,
            [Service] UserService _userService)
        {
            TokenResponse response = _userService.UserLogin(input.Phone, input.Password);
            return response;
        }
    }
}
