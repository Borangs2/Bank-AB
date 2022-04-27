using Bank_AB.Services.Users;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ViewModel(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IdentityUser? ThisUser { get; set; }
        public string[] Roles { get; set; }

        public void OnGet(string userId)
        {
            ThisUser = _userService.GetUserById(userId);
            Roles = _userService.GetUserRoles(userId).Result;




        }
    }
}
