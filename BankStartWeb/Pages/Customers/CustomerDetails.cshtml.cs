using Bank_AB.Data;
using Bank_AB.Infrastructure.Paging;
using Bank_AB.Services.Customers;
using Bank_AB.Services.Search;
using Bank_AB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Customers;

public class AccountsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ICustomerService _customerService;
    private readonly ISearchService<Account> _searchService;

    public List<AccountViewModel> Accounts = new();
    public CustomerViewModel Customer { get; set; }




    public AccountsModel(ApplicationDbContext context,
        ICustomerService customerService,
        ISearchService<Account> searchService)
    {
        _context = context;
        _customerService = customerService;
        _searchService = searchService;
    }

    public string AmountInAccounts { get; set; }

    [BindProperty(SupportsGet = true)]
    [HiddenInput]
    public string Id { get; set; }

    [BindProperty(SupportsGet = true)] 
    public string SearchTerm { get; set; }

    public int PageNum { get; set; }
    public string SortOrder { get; set; }
    public string SortCol { get; set; }
    public int TotalPageCount { get; set; }

    public void OnGet(int id, int pagenum = 1, string col = "Id", string order = "asc")
    {
        var tempCust = _customerService.GetCustomerFromId(id);
        Customer = new CustomerViewModel
        {
            Id = tempCust.Id,
            Givenname = tempCust.Givenname,
            Surname = tempCust.Surname,
            Country = tempCust.Country,
            City = tempCust.City,
            Telephone = tempCust.Telephone,
        };

        SortCol = col;
        SortOrder = order;
        PageNum = pagenum;


        var custAcc = tempCust.Accounts.AsQueryable();


        if (!string.IsNullOrEmpty(SearchTerm)) custAcc = _searchService.Search(custAcc, SearchTerm);


        var pageResult = custAcc.GetPaged(PageNum, 10);


        var paged = pageResult.Results.AsQueryable();

        pageResult.Results = paged.OrderBy(col,
            order == "asc" ? ExtensionMethods.QuerySortOrder.Asc : ExtensionMethods.QuerySortOrder.Desc).ToList();

        TotalPageCount = pageResult.PageCount;


        Accounts = pageResult.Results.Select(acc => new AccountViewModel
            {
                Id = acc.Id,
                AccountType = acc.AccountType,
                Balance = acc.Balance,
                Created = acc.Created
            })
            .ToList();

        AmountInAccounts = tempCust.Accounts.Sum(sum => sum.Balance).ToString("C");
    }
}