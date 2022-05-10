using AutoMapper;
using Bank_AB.Data;
using Bank_AB.Infrastructure.Paging;
using Bank_AB.Services.Search;
using Bank_AB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_AB.Pages.Customers;

[Authorize(Roles = "Administratör, Kassör")]
public partial class CustomersModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ISearchService<Customer> _searchService;
    private readonly IMapper _mapper;

    public CustomersModel(ApplicationDbContext context, ISearchService<Customer> searchService, IMapper mapper)
    {
        _context = context;
        _searchService = searchService;
        _mapper = mapper;
    }

    public List<CustomerViewModel> Customers { get; set; }

    [BindProperty(SupportsGet = true)] 
    public string SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)] 
    public string? SearchTermId { get; set; }

    public int PageNum { get; set; }
    public string SortOrder { get; set; }
    public string SortCol { get; set; }
    public int TotalPageCount { get; set; }


    public void OnGet(int pagenum = 1, string col = "Id", string order = "asc")
    {
        SortCol = col;
        SortOrder = order;
        PageNum = pagenum;

        var cust = _context.Customers.AsQueryable();

        if (!string.IsNullOrEmpty(SearchTerm)) cust = _searchService.Search(cust, SearchTerm);

        var pageResult = cust.GetPaged(PageNum, 10);

        var paged = pageResult.Results.AsQueryable();

        pageResult.Results = paged.OrderBy(col,
            order == "asc" ? ExtensionMethods.QuerySortOrder.Asc : ExtensionMethods.QuerySortOrder.Desc).ToList();

        TotalPageCount = pageResult.PageCount;

        Customers = pageResult.Results.Select(cust => _mapper.Map(cust, new CustomerViewModel()))
            .ToList();
    }

    public IActionResult OnPostSearchById()
    {
        if (string.IsNullOrEmpty(SearchTermId))
        {
            ModelState.AddModelError(nameof(SearchTermId), "Fältet kan inte vara tomt");
        }
        else
        {
            var cust = _context.Customers.FirstOrDefault(c => c.Id == int.Parse(SearchTermId));

            if (cust != null)
                return RedirectToPage("/Customers/CustomerDetails", new {id = cust.Id});
        }


        ModelState.AddModelError(nameof(SearchTermId), "Kund hittas ej");

        OnGet();
        return Page();
    }
}