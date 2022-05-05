using Bank_AB.Data;
using Bank_AB.Services.Users;
using Bank_AB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin;
[Authorize(Roles = "Administratör")]
public class IndexModel : PageModel
{
    public readonly ApplicationDbContext _context;
    public readonly IUserService _userService;

    public IndexModel(ApplicationDbContext context, IUserService userService)
    {
        _userService = userService;
        _context = context;
    }

    public List<IdentityUserViewModel> AllUsers { get; set; }

    public void OnGet()
    {
        GetAllUsers();
    }

    public void GetAllUsers()
    {
        AllUsers = _context.Users.Select(u => new IdentityUserViewModel
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Roles = _userService.GetUserRoles(u.Id).Result.ToList(),
        })
            .ToList();
    }
}