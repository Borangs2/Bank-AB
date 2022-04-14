using BankStartWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bank_AB.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public UserManager<IdentityUser> _userManager { get; }
        public ApplicationDbContext _context { get; }
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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

        public async Task<string[]> GetUserRoles(string id)
        {
            var roles = await _userManager.GetRolesAsync(_context.Users.First(u => u.Id == id));
            return roles.ToArray();
        }
    }
}
