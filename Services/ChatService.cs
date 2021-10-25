using BlogocomApiV2.GraphQL.Chats;
using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Services
{
    public class ChatService
    {
/*        private readonly IChat _chatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MessageService _messageService;

        public ChatService(IChat chatRepository, 
                           IHttpContextAccessor httpContextAccessor,
                           MessageService messageService)
        {
            _chatRepository = chatRepository;
            _httpContextAccessor = httpContextAccessor;
            _messageService = messageService;
        }
        async public Task<Chat> CreatePrivateChatAsync (AddPrivateChatInput input)
        {
            long userId = GetUserId();
            Chat? chat = _chatRepository.FindPrivateChat(userId, input.RecipientId);
            if(chat == null)
            {
                Chat newChat = new Chat
                {
                    UserId = userId
                };
                await 
            } else
            {

            }
        }

        public long GetUserId()
        {
            long userId = Convert.ToInt64(_httpContextAccessor.HttpContext.User.Identity.Name);
            return userId;
        }*/
    }
}
