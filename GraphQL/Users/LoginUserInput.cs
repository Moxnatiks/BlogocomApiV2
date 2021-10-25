using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Users
{
    public record LoginUserInput(
        string Phone,
        string Password);
}
