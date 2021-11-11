using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Messages
{
    public record AddMessageInput(
            long ChatId,
            string Content,
            long? [] fileIds
        );
}
