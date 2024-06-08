﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Email
{
    public interface IEmailSender
    {
        void SendEmail(EmailDTO email);
    }

}
