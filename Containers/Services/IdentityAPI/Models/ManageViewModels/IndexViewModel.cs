﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.IdentityAPI.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}