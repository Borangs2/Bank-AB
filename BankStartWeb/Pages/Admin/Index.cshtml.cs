using Bank_AB.Data;
using Bank_AB.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin;
[Authorize(Roles = "Administrat�r")]
public class IndexModel : PageModel
{
    public readonly ApplicationDbContext _context;
    public readonly IUserService _userService;

    public IndexModel(ApplicationDbContext context, IUserService userService)
    {
        _userService = userService;
        _context = context;
    }

    public List<IdentityUser> AllUsers { get; set; }

    public void OnGet()
    {
        GetAllUsers();
    }

    public void GetAllUsers()
    {
        AllUsers = _context.Users.ToList();
    }
}