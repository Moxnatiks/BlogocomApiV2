using BlogocomApiV2.Models;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using HotChocolate;

using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace BlogocomApiV2.GraphQL.Chats
{
    [GraphQLDescription("Chat Subscription")]
    [ExtendObjectType(Name = "Subscription")]

    public class ChatSubscriptions
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Chat>> OnCreatedChat(
            string token,
            [Service] ITopicEventReceiver eventReceiver,
            [Service] UserService _userService,
            CancellationToken cancellationToken)
        {
            if (_userService.ValidateToken(token))
            {
                var id = _userService.GetUserId();
                return await eventReceiver.SubscribeAsync<string, Chat>("OnCreatedChat_" + _userService.GetUserId(), cancellationToken);
            }
            else throw new ArgumentException("Invalid token!");
        }
    }
}
