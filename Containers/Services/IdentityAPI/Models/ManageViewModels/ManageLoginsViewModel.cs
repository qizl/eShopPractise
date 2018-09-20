using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.IdentityAPI.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}
