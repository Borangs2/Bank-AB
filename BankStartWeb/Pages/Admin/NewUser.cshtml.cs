using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Bank_AB.Services.Users;
using Microsoft.AspNetCore.Identity;

namespace Bank_AB.Pages.Admin
{
    public class NewModel : PageModel
    {
        private readonly IUserService _userService;

        public NewModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Namn beh�ver vara fyllt")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email beh�ver vara fyllt")]
        [BindProperty]
        [EmailAddress(ErrorMessage = "Detta �r inte en giltig email address")]
        public string Email { get; set; }
        [BindProperty]
        public string? PhoneNumber { get; set; }

        [BindProperty] 
        public bool EmailConfirmed { get; set; } = true;
        [BindProperty]
        public bool TwoFA { get; set; }

        [BindProperty]
        public bool IsCashier { get; set; } = false;

        [BindProperty]
        public bool IsAdmin { get; set; } = false;



        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser
                {
                    UserName = Name,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    EmailConfirmed = EmailConfirmed,
                    TwoFactorEnabled = TwoFA
                };

                List<string> roles = new List<string>();
                if (IsAdmin)
                    roles.Add("Administrat�r");
                if (IsCashier)
                    roles.Add("Kass�r");

                var result = _userService.CreateUser(newUser, roles.ToArray());

                if(result == IUserService.ReturnCode.UsernameAlreadyInUse)
                    ModelState.AddModelError(nameof(Name), "Anv�ndarnamnet anv�nds redan");
                if (result == IUserService.ReturnCode.EmailAlreadyInUse)
                    ModelState.AddModelError(nameof(Email), "Email adressen anv�nds redan");

                //Kolla Modelstate en sista g�ng
                if(ModelState.IsValid)
                    return RedirectToPage("Index");
            }

            return Page();

        }

    }
}
