using System.ComponentModel.DataAnnotations;
using Bank_AB.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin;

public class EditModel : PageModel
{
    private readonly IUserService _userService;


    public EditModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    [Required(ErrorMessage = "Namn behöver vara fyllt")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email behöver vara fyllt")]
    [BindProperty]
    [EmailAddress(ErrorMessage = "Detta är inte en giltig email address")]
    public string Email { get; set; }

    [BindProperty] public string? PhoneNumber { get; set; }

    [BindProperty] public bool EmailConfirmed { get; set; }

    [BindProperty] public bool TwoFA { get; set; }

    [BindProperty] public bool IsCashier { get; set; }

    [BindProperty] public bool IsAdmin { get; set; }


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

    public string[] GetRoles(string id)
    {
        var roles = _userService.GetUserRoles(id).Result;
        if (roles.Contains("Administratör"))
            IsAdmin = true;
        if (roles.Contains("Kassör"))
            IsCashier = true;
        return roles;
    }

    public IActionResult OnPost(string id)
    {
        if (ModelState.IsValid)
        {
            var user = _userService.GetUserById(id);


            user.UserName = Name;
            user.Email = Email;
            user.PhoneNumber = PhoneNumber;
            user.EmailConfirmed = EmailConfirmed;
            user.TwoFactorEnabled = TwoFA;

            var roles = new List<string>();

            if (IsAdmin)
                roles.Add("Administratör");
            if (IsCashier)
                roles.Add("Kassör");

            var result = _userService.UpdateUser(user, roles.ToArray());

            if (result == IUserService.ReturnCode.UsernameAlreadyInUse)
                ModelState.AddModelError(nameof(Name), "Användarnamnet används redan");
            if (result == IUserService.ReturnCode.EmailAlreadyInUse)
                ModelState.AddModelError(nameof(Email), "Email adressen används redan");

            //Kolla Modelstate en sista gång
            if (ModelState.IsValid)

                return RedirectToPage("ViewUser", new {userid = id});
        }

        return Page();
    }
}