using System.ComponentModel.DataAnnotations;

namespace EnjoyCodes.eShopOnContainers.Services.IdentityAPI.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
