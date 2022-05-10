using System.ComponentModel.DataAnnotations;
using Bank_AB.Data;
using Bank_AB.Infrastructure.Attributes;
using Bank_AB.Services.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Transactions;

[BindProperties]
public class TransferModel : PageModel
{
    private readonly ITransactionsService _transactionsService;

    public TransferModel(ApplicationDbContext context, ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    public int AccountId { get; set; }

    [Required(ErrorMessage = "Ange ett nummer")]
    [Range(1, int.MaxValue, ErrorMessage = "Ange ett nummer större än 0")]
    [IsNumeric(ErrorMessage = "Ange ett nummer större än 0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Ange ett konto")]
    [Range(1, int.MaxValue, ErrorMessage = "Ange ett kontonummer större än 0")]
    public int TransAccountId { get; set; }

    public void OnGet(int accountId)
    {
        AccountId = accountId;
    }


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
                return RedirectToPage("/Customers/AccountDetails", new {id = accountId});
        }


        return Page();
    }
}