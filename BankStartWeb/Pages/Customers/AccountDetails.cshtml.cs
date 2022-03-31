using Bank_AB.Services;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class TransactionsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransactionsModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public class TransactionViewModel
        {
            public int Id { get; set; }
            public string Operation { get; set; } = null!;
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
        }

        public int CustomerId { get; set; }
        public Account Account { get; set; }
        public List<TransactionViewModel> Transactions = new List<TransactionViewModel>();

        public void OnGet(int id)
        {
            Account = _accountService.GetAccountFromId(id);

            CustomerId = _context.Customers.First(cust => cust.Accounts.Any(acc => acc.Id == id)).Id;

            Transactions = Account.Transactions.Select(trans => new TransactionViewModel
            {
                Id = trans.Id,
                Operation = trans.Operation,
                Date = trans.Date,
                Amount = trans.Amount,
                NewBalance = trans.NewBalance
            })
                .OrderByDescending(o => o.Date).ToList();


        }
    }
}
