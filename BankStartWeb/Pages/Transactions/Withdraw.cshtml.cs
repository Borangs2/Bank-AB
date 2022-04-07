using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Transactions
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public WithdrawModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AccountId { get; set; }
        public void OnGet(int accountId)
        {
            AccountId = accountId;
        }

    }
}
