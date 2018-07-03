using System.Security.Principal;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
