using System.ComponentModel.DataAnnotations;

namespace MainSite.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }
    }
}
