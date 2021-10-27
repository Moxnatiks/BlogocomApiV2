using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Repository
{
    public class MessageRepository : IMessage
    {
        private readonly ApiDbContext DB;
        public MessageRepository(ApiDbContext apiDbContext)
        {
            DB = apiDbContext;
        }

        public bool CheckUserAccessToMessage(long userId, long messageId)
        {
            Message message = DB.Messages.FirstOrDefault(i => i.Id == messageId);
            if (message != null)
            {
                if (message.UserId == userId)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public Message GetMessageById(long messageId)
        {
            Message message = DB.Messages.FirstOrDefault(i => i.Id == messageId);
            if (message != null)
            {
                return message;
            }
            else throw new ArgumentNullException("Object not found!");
        }
    }
}
