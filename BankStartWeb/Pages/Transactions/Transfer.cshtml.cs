using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Bank_AB.Pages.Transactions
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public TransferModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int AccountId { get; set; }
        public void OnGet(int accountId)
        {
            AccountId = accountId;
            SetAllAccounts();
        }

        [Range(1, Int32.MaxValue, ErrorMessage = "Ange ett nummer större än 0")]
        public decimal Amount { get; set; }
        public int TransAccountId { get; set; }

        public List<SelectListItem> AllAccounts { get; set; }


        public IActionResult OnPost(int accountId)
        {
            Account account = _context.Accounts.Include(trans => trans.Transactions).First(acc => acc.Id == AccountId);

            if (TransactionVerification.CheckForOvercharge(Amount, account.Balance))
            {
                ModelState.AddModelError(nameof(Amount), "Vald summa är större än vad som finns på kontot");
            }

            if (ModelState.IsValid)
            {
                Account transAccount = _context.Accounts.Include(trans => trans.Transactions).First(acc => acc.Id == TransAccountId);

                var withdrawTransaction = new Transaction
                {
                    Type = "Transfer",
                    Operation = "Transfer",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = account.Balance - Amount
                };

                var depositTransaction = new Transaction
                {
                    Type = "Transfer",
                    Operation = "Transfer",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = transAccount.Balance + Amount,
                };

                account.Balance -= Amount;
                transAccount.Balance += Amount;


                account.Transactions.Add(withdrawTransaction);
                transAccount.Transactions.Add(depositTransaction);

                _context.SaveChanges();
                return RedirectToPage("/Customers/AccountDetails", new { id = accountId });
            }


            SetAllAccounts();
            return Page();
        }

        public void SetAllAccounts()
        {
            var cust = _context.Customers.Include(cust => cust.Accounts).First(cust => cust.Accounts.Any(acc => acc.Id == AccountId));

            AllAccounts = new List<SelectListItem>();
            foreach (var account in cust.Accounts)
            {
                AllAccounts.Add(new SelectListItem
                {
                    Text = $"Konto { account.Id }",
                    Value = account.Id.ToString()
                });
            }
        }
    }
}
