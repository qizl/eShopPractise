using System.ComponentModel.DataAnnotations;

namespace EnjoyCodes.eShopOnContainers.Services.IdentityAPI.Models.ManageViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
