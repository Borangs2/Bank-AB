using System.Runtime.CompilerServices;
using Bank_AB.Services.Users;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bank_AB.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public readonly IUserService _userService;
        public readonly ApplicationDbContext _context;
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
}
