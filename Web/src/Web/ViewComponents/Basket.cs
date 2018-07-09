using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Infrastructure.Identity;
using EnjoyCodes.eShopOnWeb.Web.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnjoyCodes.eShopOnWeb.Web.ViewComponents
{
    public class Basket
    {
        private readonly IBasketViewModelService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;
    }
}
