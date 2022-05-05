using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Bank_AB.ViewModels
{
    public class IdentityUserEditViewModel
    {
        [HiddenInput]
        public string Id { get; set; }
        [Required(ErrorMessage = "Namn behöver vara fyllt")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email behöver vara fyllt")]
        [EmailAddress(ErrorMessage = "Detta är inte en giltig email address")]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool TwoFA { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
