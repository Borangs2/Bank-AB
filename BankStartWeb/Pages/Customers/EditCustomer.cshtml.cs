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
        [MaxLength(50, ErrorMessage = "Namnet är är för långt")]
        [Required(ErrorMessage = "Förnamn är obligatoriskt")]
        public string Givenname { get; set; } = null!;
        [BindProperty]
        [MaxLength(50, ErrorMessage = "Namnet är är för långt")]
        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        public string Surname { get; set; } = null!;
        [BindProperty]
        [MaxLength(100, ErrorMessage = "Namnet är för långt")]
        [Required(ErrorMessage = "Land är obligatoriskt")]
        public string Country { get; set; } = null!;
        [BindProperty]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Fältet måste innehålla ett telefonnummer")]
        public string Telephone { get; set; } = null!;
        [BindProperty]
        [DataType(DataType.EmailAddress, ErrorMessage = "Fältet måste innehålla en mejladress")]
        [Required(ErrorMessage = "Mejladress är obligatoriskt")]
        public string EmailAddress { get; set; } = null!;
        [BindProperty]
        [MaxLength(90, ErrorMessage = "Namnet är för långt")]
        [Required(ErrorMessage = "Stad är obligatoriskt")]
        public string City { get; set; } = null!;
        [BindProperty]
        [MaxLength(60, ErrorMessage = "Namnet är för långt")]
        [Required(ErrorMessage = "Hemadress är obligatoriskt")]
        public string Adress { get; set; } = null!;
        [BindProperty]
        [MaxLength(30, ErrorMessage = "Personnumret är för långt")]
        [Required(ErrorMessage = "Personnummer är obligatoriskt")]
        public string NationalId { get; set; } = null!;
        [BindProperty]
        [MaxLength(10, ErrorMessage = "Postnumret är för långt")]
        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        public string Zipcode { get; set; } = null!;
        [BindProperty]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Födelsedatum är obligatoriskt")]
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

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var editCust = _mapper.Map(this, Customer);
                var result = _customerService.EditCustomer(editCust, id);

                if (result == ICustomerService.ReturnCode.InvalidCountry)
                    ModelState.AddModelError(Country, "Valt land är inte giltigt");

                if (ModelState.IsValid)
                {
                    _toastNotification.AddSuccessToastMessage("Ändringar sparade");
                    return RedirectToPage("/Customers/CustomerDetails", new { id = id });
                }
            }

            return Page();
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
