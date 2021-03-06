using BlogocomApiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Interfaces
{
    public interface IMessage
    {
        Task<Message> CreateMessageAsync(Message message);
        bool CheckUserAccessToMessage(long userId, long messageId);
        Message GetMessageById(long messageId);


    }
}
