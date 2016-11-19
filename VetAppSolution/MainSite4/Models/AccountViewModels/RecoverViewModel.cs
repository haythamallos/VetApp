using System.ComponentModel.DataAnnotations;

namespace MainSite.Models.AccountViewModels
{
    public class RecoverViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
