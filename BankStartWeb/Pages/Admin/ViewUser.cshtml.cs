using Bank_AB.Data;
using Bank_AB.Services.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin;

public class ViewModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public ViewModel(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }


    public ViewUserViewModel ThisUser { get; set; }
    public string[] Roles { get; set; }

    public void OnGet(string userId)
    {
        var user = _userService.GetUserById(userId);

        ThisUser = new ViewUserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            TwoFA = user.TwoFactorEnabled,
            Roles = _userService.GetUserRoles(userId).Result
        };
    }

    public class ViewUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFA { get; set; }
        public string[] Roles { get; set; }
    }
}