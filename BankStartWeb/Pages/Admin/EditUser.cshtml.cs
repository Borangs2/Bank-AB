using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Bank_AB.Services.Users;
using Microsoft.AspNetCore.Identity;

namespace Bank_AB.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(IUserService userService, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [BindProperty]
        [Required(ErrorMessage = "Namn behöver vara fyllt")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email behöver vara fyllt")]
        [BindProperty]
        [EmailAddress(ErrorMessage = "Detta är inte en giltig email address")]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public bool EmailConfirmed { get; set; }
        [BindProperty]
        public bool TwoFA { get; set; }
        [BindProperty]
        public bool IsCashier { get; set; }
        [BindProperty]
        public bool IsAdmin { get; set; }






        

        public IdentityUser ThisUser { get; set; }




        public void OnGet(string id)
        {
            GetRoles(id);
            ThisUser = _userService.GetUserById(id);
            Name = ThisUser.UserName;
            Email = ThisUser.Email;
            PhoneNumber = ThisUser.PhoneNumber;
            EmailConfirmed = ThisUser.EmailConfirmed;
            TwoFA = ThisUser.TwoFactorEnabled;
        }

        public void GetRoles(string id)
        {
            var roles = _userService.GetUserRoles(id).Result;
            if (roles.Contains("Administratör"))
                IsAdmin = true;
            if (roles.Contains("Kassör"))
                IsCashier = true;
        }
    }
}
