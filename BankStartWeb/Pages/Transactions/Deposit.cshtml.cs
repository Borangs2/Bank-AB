using Bank_AB.Infrastructure.Attributes;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bank_AB.Services.Transactions;

namespace Bank_AB.Pages.Transactions
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly ITransactionsService _transactionService;
        private readonly ApplicationDbContext _context;
        public DepositModel(ApplicationDbContext context, ITransactionsService transactionsService)
        {
            _context = context;
            _transactionService = transactionsService;
        }

        public int AccountId { get; set; }
        public void OnGet(int accountId)
        {
            AccountId = accountId;
            SetAllSelectLists();
        }

        [Range(1, Int32.MaxValue, ErrorMessage = "Ange ett värde större än 0")]
        [IsNumeric(ErrorMessage = "Ange ett värde större än 0")]
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }

        public List<SelectListItem> AllTypes { get; set; }
        public List<SelectListItem> AllOperations { get; set; }


        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionService.Deposit(AccountId, Amount, Operation, Type);

                if (status == ITransactionsService.ReturnCode.ValueNegative)
                    ModelState.AddModelError(nameof(Amount), "Värdet kan inte vara negativt");
                

                if(status == ITransactionsService.ReturnCode.Ok)
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
                    Value = "Deposit Cash",
                    Text = "Insättning"
                },
                new SelectListItem
                {
                    Value = "Salary",
                    Text = "Lön"
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
