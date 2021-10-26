﻿using BlogocomApiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Interfaces
{
    public interface IChat
    {
        bool CheckUserAccessToChat(long userId, long chatId);
    }
}
