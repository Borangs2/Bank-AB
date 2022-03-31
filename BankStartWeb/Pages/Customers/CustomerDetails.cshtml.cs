using Bank_AB.Services;
using Bank_AB.Services.Search;
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
        private readonly ISearchService<Account> _searchService;

        public AccountsModel(ApplicationDbContext context,
            ICustomerService customerService,
            ISearchService<Account> searchService)
        {
            _context = context;
            _customerService = customerService;
            _searchService = searchService;
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
        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public string id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet(int id)
        {
            Customer = _customerService.GetCustomerFromId(id);

            var acc = Customer.Accounts.AsQueryable();
                


            if (!string.IsNullOrEmpty(SearchTerm))
            {
                acc = _searchService.Search(acc, SearchTerm);
            }

            Accounts = acc.Select(acc => new AccountsViewModel
            {
                Id = acc.Id,
                AccountType = acc.AccountType,
                Balance = acc.Balance,
                Created = acc.Created
            })
            .ToList();

            AmountInAccounts = Customer.Accounts.Sum(sum => sum.Balance).ToString("C");
        }
    }
}
