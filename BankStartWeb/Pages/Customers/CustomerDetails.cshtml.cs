using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class AccountsModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public AccountsModel(ApplicationDbContext context)
        {
            _context = context;
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

        public void OnGet(int id)
        {
            Customer = _context.Customers.Include(c => c.Accounts).First(cust => cust.Id == id);

            Accounts = Customer.Accounts.Select(acc => new AccountsViewModel
            {
                Id = acc.Id,
                AccountType = acc.AccountType,
                Balance = acc.Balance,
                Created = acc.Created
            }).ToList();
        }
    }
}
