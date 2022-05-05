using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bank_AB.Services.Users;
using Bank_AB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin;

public class EditModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;


    public EditModel(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [BindProperty] public bool IsCashier { get; set; }

    [BindProperty] public bool IsAdmin { get; set; }

    [BindProperty]
    public IdentityUserEditViewModel ThisUser { get; set; }


    public void OnGet(string id)
    {
        GetRoles(id);
        var tempUser = _userService.GetUserById(id);

        //AutoMapper
        ThisUser = _mapper.Map(tempUser, ThisUser);
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
            var user = _userService.GetUserById(ThisUser.Id);

            //AutoMapper
            user = _mapper.Map(ThisUser, user);

            var roles = new List<string>();

            if (IsAdmin)
                roles.Add("Administratör");
            if (IsCashier)
                roles.Add("Kassör");

            var result = _userService.UpdateUser(user, roles.ToArray());

            if (result == IUserService.ReturnCode.UsernameAlreadyInUse)
                ModelState.AddModelError(nameof(ThisUser.UserName), "Användarnamnet används redan");
            if (result == IUserService.ReturnCode.EmailAlreadyInUse)
                ModelState.AddModelError(nameof(ThisUser.Email), "Email adressen används redan");

            //Kolla Modelstate en sista gång
            if (ModelState.IsValid)

                return RedirectToPage("ViewUser", new {userid = ThisUser.Id});
        }

        return Page();
    }
}