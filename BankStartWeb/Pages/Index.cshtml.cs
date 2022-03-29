using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public int NumCustomers { get; set; }
        public int NumAccounts { get; set; }
        public string AmountInAccounts { get; set; }




        public void OnGet()
        {
            NumCustomers = _context.Customers.Count();
            NumAccounts = _context.Accounts.Count();
            AmountInAccounts = string.Format("{0:0,0.00}", _context.Accounts.Sum(sum => sum.Balance));
        }
    }
}