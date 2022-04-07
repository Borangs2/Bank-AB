using Bank_AB.Infrastructure.Paging;
using Bank_AB.Services;
using Bank_AB.Services.Search;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class TransactionsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly ISearchService<Transaction> _searchService;

        public TransactionsModel(ApplicationDbContext context, 
            IAccountService accountService,
            ISearchService<Transaction> searchService)
        {
            _context = context;
            _accountService = accountService;
            _searchService = searchService;
        }

        public class TransactionViewModel
        {
            public int Id { get; set; }
            public string Operation { get; set; } = null!;
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
        }

        public int CustomerId { get; set; }
        public Account Account { get; set; }
        public List<TransactionViewModel> Transactions = new List<TransactionViewModel>();

        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public string Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public int PageNum { get; set; }
        public string SortOrder { get; set; }
        public string SortCol { get; set; }
        public int TotalPageCount { get; set; }
        public void OnGet(int id, int pagenum = 1, string col = "Date", string order = "desc")
        {
            Account = _accountService.GetAccountFromId(id);

            CustomerId = _context.Customers.First(cust => cust.Accounts.Any(acc => acc.Id == id)).Id;

            SortCol = col;
            SortOrder = order;
            PageNum = pagenum;


            var trans = Account.Transactions.AsQueryable();


            if (!string.IsNullOrEmpty(SearchTerm))
            {
                trans = _searchService.Search(trans, SearchTerm);
            }

            var pageResult = trans.GetPaged(PageNum, 10);


            var paged = pageResult.Results.AsQueryable();

            pageResult.Results = paged.OrderBy(col,
                            order == "asc" ? ExtensionMethods.QuerySortOrder.Asc :
                            ExtensionMethods.QuerySortOrder.Desc).ToList();

            TotalPageCount = pageResult.PageCount;

            Transactions = pageResult.Results.Select(trans => new TransactionViewModel
            {
                Id = trans.Id,
                Operation = trans.Operation,
                Date = trans.Date,
                Amount = trans.Amount,
                NewBalance = trans.NewBalance
            })
                .ToList();
        }
    }
}
