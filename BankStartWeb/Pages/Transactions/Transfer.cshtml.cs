using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Transactions
{
    public class TransferModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public TransferModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int AccountId { get; set; }
        public void OnGet(int customerId, int accountId)
        {
            AccountId = accountId;
        }

    }
}
