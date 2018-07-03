﻿using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
