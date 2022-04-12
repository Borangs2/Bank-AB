using Bank_AB.Services;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            //Account account = _context.Accounts.Include(trans => trans.Transactions).First(acc => acc.Id == AccountId);

            //if (TransactionVerification.CheckForOvercharge(Amount, account.Balance))
            //    ModelState.AddModelError(nameof(Amount), "Vald summa är större än vad som finns på kontot");


            if (ModelState.IsValid)
            {
                //var transaction = new Transaction
                //{
                //    Type = Type,
                //    Operation = Operation,
                //    Date = DateTime.Now,
                //    Amount = Amount,
                //    NewBalance = account.Balance - Amount
                //};
                //account.Balance -= Amount;

                //account.Transactions.Add(transaction);
                //_context.SaveChanges();

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
