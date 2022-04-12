using Bank_AB.Infrastructure.Attributes;
using Bank_AB.Services;
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
        private readonly ITransactionsService _transactionsService;

        public TransferModel(ApplicationDbContext context, ITransactionsService transactionsService)
        {
            _context = context;
            _transactionsService = transactionsService;
        }
        public int AccountId { get; set; }
        public void OnGet(int accountId)
        {
            AccountId = accountId;
            SetAllAccounts();
        }

        [Range(1, Int32.MaxValue, ErrorMessage = "Ange ett nummer större än 0")]
        [IsNumeric(ErrorMessage = "Ange ett nummer större än 0")]
        public decimal Amount { get; set; }
        public int TransAccountId { get; set; }

        public List<SelectListItem> AllAccounts { get; set; }


        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionsService.Transfer(AccountId, TransAccountId, Amount);

                if (status == ITransactionsService.ReturnCode.ValueNegative)
                    ModelState.AddModelError(nameof(Amount), "Värdet kan inte vara negativt");
                if (status == ITransactionsService.ReturnCode.BalanceToLow)
                    ModelState.AddModelError(nameof(Amount), "Inte tillräckligt saldo på kontot");

                if(status == ITransactionsService.ReturnCode.Ok)
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
