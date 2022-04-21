using Bank_AB.Infrastructure.Attributes;
using Bank_AB.Services.Transactions;
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
        }

        [Range(1, Int32.MaxValue, ErrorMessage = "Ange ett nummer större än 0")]
        [IsNumeric(ErrorMessage = "Ange ett nummer större än 0")]
        public decimal Amount { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Ange ett kontonummer större än 0")]
        public int TransAccountId { get; set; }


        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionsService.Transfer(AccountId, TransAccountId, Amount);

                if (status == ITransactionsService.ReturnCode.ValueNegative)
                    ModelState.AddModelError(nameof(Amount), "Värdet kan inte vara negativt");
                if (status == ITransactionsService.ReturnCode.BalanceToLow)
                    ModelState.AddModelError(nameof(Amount), "Inte tillräckligt saldo på kontot");
                if (status == ITransactionsService.ReturnCode.NotFound)
                    ModelState.AddModelError(nameof(TransAccountId), "Kontot hittades ej");

                if (status == ITransactionsService.ReturnCode.Ok)
                    return RedirectToPage("/Customers/AccountDetails", new { id = accountId });
            }


            return Page();
        }
    }
}
