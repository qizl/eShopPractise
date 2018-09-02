using Microsoft.AspNetCore.Http;
using System;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity() => this._context.HttpContext.User.FindFirst("sub").Value;
    }
}
