namespace EnjoyCodes.eShopOnContainers.Services.IdentityAPI.Services
{
    public interface IRedirectService
    {
        string ExtractRedirectUriFromReturnUrl(string url);
    }
}
