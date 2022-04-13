using BankStartWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public ApplicationDbContext _context { get; }
        public IndexModel(ApplicationDbContext context)
        {
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
