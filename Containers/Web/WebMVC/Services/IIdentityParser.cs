using System.Security.Principal;

namespace EnjoyCodes.eShopOnContainers.WebMVC.Services
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
