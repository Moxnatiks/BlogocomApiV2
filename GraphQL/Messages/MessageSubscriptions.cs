using BlogocomApiV2.Models;
using BlogocomApiV2.Services;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Messages
{
    [GraphQLDescription("Message Subscription")]
    [ExtendObjectType(Name = "Subscription")]
    public class MessageSubscriptions
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Message>> OnCreatedMessage(
           string token,
           [Service] ITopicEventReceiver eventReceiver,
           [Service] UserService _userService,
           CancellationToken cancellationToken)
        {
            if (_userService.ValidateToken(token))
            {
                var id = _userService.GetUserId();
                return await eventReceiver.SubscribeAsync<string, Message>("OnCreatedMessage_" + _userService.GetUserId(), cancellationToken);
            }
            else throw new ArgumentException("Invalid token!");
        }
    }
}
