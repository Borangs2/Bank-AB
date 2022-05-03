using Bank_AB.Infrastructure.Attributes;
using Bank_AB.Services.Transactions;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bank_AB.Data;

namespace Bank_AB.Pages.Transactions
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransactionsService _transactionsService;

        public WithdrawModel(ApplicationDbContext context, ITransactionsService transactionsService)
        {
            _context = context;
            _transactionsService = transactionsService;
        }

        public int AccountId { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Ange ett nummer större än 0")]
        [IsNumeric(ErrorMessage = "Ange ett värde större än 0")]
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }

        public List<SelectListItem> AllTypes { get; set; }
        public List<SelectListItem> AllOperations { get; set; }


        public void OnGet(int accountId)
        {
            AccountId = accountId;
            SetAllSelectLists();
        }


        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionsService.Withdraw(AccountId, Amount, Operation, Type);

                if (status == ITransactionsService.ReturnCode.ValueNegative)
                    ModelState.AddModelError(nameof(Amount), "Värdet kan inte vara negativt");
                if (status == ITransactionsService.ReturnCode.BalanceToLow)
                    ModelState.AddModelError(nameof(Amount), "Inte tillräckligt saldo på kontot");


                if (status == ITransactionsService.ReturnCode.Ok)
                    return RedirectToPage("/Customers/AccountDetails", new { id = accountId });
            }


            SetAllSelectLists();
            return Page();
        }

        public void SetAllSelectLists()
        {
            SetAllTypes();
            SetAllOperations();
        }

        private void SetAllTypes()
        {
            AllTypes = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = "Debit",
                    Text = "Debit"
                },
                new SelectListItem
                {
                    Value = "Credit",
                    Text = "Kredit"
                }
            };
        }

        private void SetAllOperations()
        {
            AllOperations = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = "ATM Withdrawal",
                    Text = "Uttagsautomat"
                },
                new SelectListItem
                {
                    Value = "Payment",
                    Text = "Betalning"
                },
                new SelectListItem
                {
                    Value = "Transfer",
                    Text = "Överföring"
                }
            };
        }
    }

}
