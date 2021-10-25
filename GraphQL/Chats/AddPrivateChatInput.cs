using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Chats
{
    public record AddPrivateChatInput (
        long RecipientId);
}
