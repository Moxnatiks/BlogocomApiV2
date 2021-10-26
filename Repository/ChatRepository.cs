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
        private readonly ApiDbContext DB;
        public ChatRepository(ApiDbContext apiDbContext)
        {
            DB = apiDbContext;
        }
        public bool CheckUserAccessToChat(long userId, long chatId)
        {
            long[] idsUsers = DB.UserChats.Where(u => u.UserId == userId).Select(i => i.ChatId).ToArray();
            long? index = Array.IndexOf(idsUsers, chatId);

            if (index != null) return true;
            else return false;


        }
    }
}
