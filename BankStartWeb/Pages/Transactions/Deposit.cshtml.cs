using System.ComponentModel.DataAnnotations;
using Bank_AB.Data;
using Bank_AB.Infrastructure.Attributes;
using Bank_AB.Services.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bank_AB.Pages.Transactions;

[BindProperties]
public class DepositModel : PageModel
{
    private readonly ITransactionsService _transactionService;

    public DepositModel(ITransactionsService transactionsService)
    {
        _transactionService = transactionsService;
    }

    public int AccountId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Ange ett värde större än 0")]
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
            var status = _transactionService.Deposit(AccountId, Amount, Operation, Type);

            if (status == ITransactionsService.ReturnCode.ValueNegative)
                ModelState.AddModelError(nameof(Amount), "Värdet kan inte vara negativt");


            if (status == ITransactionsService.ReturnCode.Ok)
                return RedirectToPage("/Customers/AccountDetails", new {id = accountId});
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
        AllTypes = new List<SelectListItem>
        {
            new()
            {
                Value = "Debit",
                Text = "Debit"
            },
            new()
            {
                Value = "Credit",
                Text = "Kredit"
            }
        };
    }

    private void SetAllOperations()
    {
        AllOperations = new List<SelectListItem>
        {
            new()
            {
                Value = "Deposit Cash",
                Text = "Insättning"
            },
            new()
            {
                Value = "Salary",
                Text = "Lön"
            },
            new()
            {
                Value = "Transfer",
                Text = "Överföring"
            }
        };
    }
}