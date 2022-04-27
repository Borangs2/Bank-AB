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
        [Required(ErrorMessage = "Namn behöver vara fyllt")]
        public string Name { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lösenord måste vara fyllt")]
        public string Password { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Detta fält måste vara fyllt")]
        [Compare(nameof(Password), ErrorMessage = "Lösenorden stämmer inte överens")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Email behöver vara fyllt")]
        [BindProperty]
        [EmailAddress(ErrorMessage = "Detta är inte en giltig email address")]
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
                    PasswordHash = Password,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    EmailConfirmed = EmailConfirmed,
                    TwoFactorEnabled = TwoFA
                };

                List<string> roles = new List<string>();
                if (IsAdmin)
                    roles.Add("Administratör");
                if (IsCashier)
                    roles.Add("Kassör");

                var result = _userService.CreateUser(newUser, roles.ToArray());

                switch (result)
                {
                    case IUserService.ReturnCode.UsernameAlreadyInUse:
                        ModelState.AddModelError(nameof(Name), "Användarnamnet används redan");
                        break;
                    case IUserService.ReturnCode.EmailAlreadyInUse:
                        ModelState.AddModelError(nameof(Email), "Email adressen används redan");
                        break;
                    case IUserService.ReturnCode.InvalidUsername:
                        ModelState.AddModelError(nameof(Name), "Användarnamnet är ogiltigt. Användarnamnet kan bara innehålla bokstäver mellan a-z och siffror från 0-9");
                        break;
                    case IUserService.ReturnCode.InvalidPassword:
                        ModelState.AddModelError(nameof(Password), "Lösenordet är ogiltigt. Lösenordet behöver innehålla åtminstone 1 stor och lite bokstav och minst 1 siffra");
                        break;
                    case IUserService.ReturnCode.InvalidUsernameOrPassword:
                        ModelState.AddModelError(nameof(Name), "Användarnamnet eller lösenordet är ogiltigt");
                        break;
                }


                //Kolla Modelstate en sista gång
                if (ModelState.IsValid)
                    return RedirectToPage("Index");
            }

            return Page();

        }

    }
}
