using Bank_AB.Services;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class AccountsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly ICustomerService _customerService;

        public AccountsModel(ApplicationDbContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        public class AccountsViewModel
        {
            public int Id { get; set; }
            public string AccountType { get; set; } = null!;

            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }

        public List<AccountsViewModel> Accounts = new List<AccountsViewModel>();
        public Customer Customer { get; set; }
        public string AmountInAccounts { get; set; }

        public void OnGet(int id)
        {
            Customer = _customerService.GetCustomerFromId(id);

            Accounts = Customer.Accounts.Select(acc => new AccountsViewModel
            {
                Id = acc.Id,
                AccountType = acc.AccountType,
                Balance = acc.Balance,
                Created = acc.Created
            }).ToList();

            AmountInAccounts = Customer.Accounts.Sum(sum => sum.Balance).ToString("C");
        }
    }
}
