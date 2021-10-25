using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Repository
{
    public class ChatRepository : IChat
    {
        /*private readonly ApiDbContext DB;
        public ChatRepository(ApiDbContext apiDbContext)
        {
            DB = apiDbContext;
        }

        async public Task<Chat> CreateChatAsync(Chat chat)
        {
            DB.Chats.AddAsync(chat);
            await context.SaveChangesAsync(cancellationToken);
        }

        public Chat? FindPrivateChat(long firstUserId, long secondUserId)
        {
            User user1 = DB.Users.FirstOrDefault(u => u.Id == firstUserId);
            User user2 = DB.Users.FirstOrDefault(u => u.Id == secondUserId);
            if (user1 != null && user2 != null)
            {
                long[] ids1 = DB.UserChats.Where(u => u.UserId == firstUserId).Select(i => i.ChatId).ToArray();
                long[] ids2 = DB.UserChats.Where(u => u.UserId == secondUserId).Select(i => i.ChatId).ToArray();

                long []  chatIds = ids1.Intersect(ids2).ToArray();

                if (chatIds != null)
                {
                    Chat? chat = DB.Chats.FirstOrDefault(c => c.Id == chatIds[0]);
                    return chat;
                }
                else return null;
            } 
            else return null;
            
        }*/
    }
}
