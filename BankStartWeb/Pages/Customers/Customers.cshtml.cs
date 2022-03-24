using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers
{
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Customer> Customers { get; set; }

        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Givenname { get; set; } = null!;
            public string Surname { get; set; } = null!;
            public string Country { get; set; } = null!;
            public string Telephone { get; set; } = null!;
            public string EmailAddress { get; set; } = null!;
            public string City { get; set; } = null!;
        }

        public void OnGet()
        {
            Customers = _context.Customers.Take(20).Select(cust => new Customer
            {

                
                Id = cust.Id,
                Givenname = cust.Givenname,
                Surname = cust.Surname,
                Country = cust.Country,
                Telephone = cust.Telephone,
                EmailAddress = cust.EmailAddress,
                City = cust.City
            }).ToList();


        }
    }
}
