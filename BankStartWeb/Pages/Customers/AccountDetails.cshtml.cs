using AutoMapper;
using Bank_AB.Data;
using Bank_AB.Infrastructure.Paging;
using Bank_AB.Services.Accounts;
using Bank_AB.Services.Search;
using Bank_AB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Customers;

public class TransactionsModel : PageModel
{
    private readonly IAccountService _accountService;

    private readonly ApplicationDbContext _context;
    private readonly ISearchService<Transaction> _searchService;
    private readonly IMapper _mapper;
    public List<TransactionViewModel> Transactions = new();

    public TransactionsModel(ApplicationDbContext context,
        IAccountService accountService,
        ISearchService<Transaction> searchService,
        IMapper mapper)
    {
        _context = context;
        _accountService = accountService;
        _searchService = searchService;
        _mapper = mapper;
    }

    public int CustomerId { get; set; }
    public Account Account { get; set; }

    [BindProperty(SupportsGet = true)]
    [HiddenInput]
    public string Id { get; set; }

    [BindProperty(SupportsGet = true)] public string SearchTerm { get; set; }

    public int PageNum { get; set; }
    public string SortOrder { get; set; }
    public string SortCol { get; set; }

    public void OnGet(int id, int pagenum = 1, string col = "Date", string order = "desc")
    {
        Account = _accountService.GetAccountFromId(id);

        CustomerId = _context.Customers.First(cust => cust.Accounts.Any(acc => acc.Id == id)).Id;

        SortCol = col;
        SortOrder = order;
        PageNum = pagenum;
    }

    public IActionResult OnGetViewMore(int id, string searchTerm, int pageNum = 1, string col = "Date",
        string order = "desc")
    {
        var query = _context.Accounts.Where(acc => acc.Id == id)
            .SelectMany(t => t.Transactions);


        if (!string.IsNullOrEmpty(searchTerm)) query = _searchService.Search(query, searchTerm);

        query = query.OrderByDescending(o => o.Date);

        query = query.OrderBy(col,
            order == "asc" ? ExtensionMethods.QuerySortOrder.Asc : ExtensionMethods.QuerySortOrder.Desc).AsQueryable();


        var pageResult = query.GetPaged(pageNum, 10);

        Transactions = pageResult.Results.Select(trans => _mapper.Map(trans, new TransactionViewModel()))
            .ToList();

        return new JsonResult(new {items = Transactions});
        ;
    }

}