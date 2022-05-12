using Bank_AB.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bank_AB.Data;

namespace Bank_AB.Pages.Customers
{
    [Authorize(Roles = "Administratör, Kassör")]
    public class EditCustomerModel : PageModel
    {

        private readonly ICustomerService _customerService;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public EditCustomerModel(ICustomerService customerService, IToastNotification toastNotification, IMapper mapper)
        {
            _customerService = customerService;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        [BindProperty]
        public string Givenname { get; set; } = null!;
        [BindProperty]
        public string Surname { get; set; } = null!;
        [BindProperty]
        public string Country { get; set; } = null!;
        [BindProperty]
        public string Telephone { get; set; } = null!;
        [BindProperty]
        public string EmailAddress { get; set; } = null!;
        [BindProperty]
        public string City { get; set; } = null!;
        [BindProperty]
        public string Adress { get; set; } = null!;
        [BindProperty]
        public string NationalId { get; set; } = null!;
        [BindProperty]
        public string Zipcode { get; set; } = null!;
        [BindProperty]
        [HiddenInput]
        public DateTime Birthday { get; set; }
        [BindProperty]
        [HiddenInput]
        public string Id { get; set; }
        public Customer Customer { get; set; }

        public List<SelectListItem> Countries { get; set; }
        public IActionResult OnGet(int id)
        {
            Customer = _customerService.GetCustomerFromId(id);
            if (Customer == null)
            {
                return RedirectToPage("/Customers/Customers");
            }

            _mapper.Map(Customer, this);
            
            SetCountries();
            return Page();
        }

        //TODO: Add toast notifications
        public IActionResult OnPost(int id)
        {
            var editCust = _mapper.Map(this, Customer);
            var result = _customerService.EditCustomer(editCust, id);

            return RedirectToPage("/Customers/CustomerDetails", new{id = id});
        }


        private void SetCountries()
        {
            Countries = new List<SelectListItem>
            {
                new() {Text = "Sverige", Value="Sverige"},
                new() {Text = "Norge", Value="Norge"},
                new() {Text = "Finland", Value="Finland"},
            };
        }
    }
}
