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
    [Required(ErrorMessage = "Namn beh�ver vara fyllt")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email beh�ver vara fyllt")]
    [BindProperty]
    [EmailAddress(ErrorMessage = "Detta �r inte en giltig email address")]
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
        if (roles.Contains("Administrat�r"))
            IsAdmin = true;
        if (roles.Contains("Kass�r"))
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
                roles.Add("Administrat�r");
            if (IsCashier)
                roles.Add("Kass�r");

            var result = _userService.UpdateUser(user, roles.ToArray());

            if (result == IUserService.ReturnCode.UsernameAlreadyInUse)
                ModelState.AddModelError(nameof(Name), "Anv�ndarnamnet anv�nds redan");
            if (result == IUserService.ReturnCode.EmailAlreadyInUse)
                ModelState.AddModelError(nameof(Email), "Email adressen anv�nds redan");

            //Kolla Modelstate en sista g�ng
            if (ModelState.IsValid)

                return RedirectToPage("ViewUser", new {userid = id});
        }

        return Page();
    }
}