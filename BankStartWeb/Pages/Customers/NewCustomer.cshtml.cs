using System.ComponentModel.DataAnnotations;
using Bank_AB.Data;
using Bank_AB.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace Bank_AB.Pages.Customers
{
    [Authorize(Roles = "Administratör, Kassör")]
    public class NewCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IToastNotification _toastNotification;

        public NewCustomerModel(ICustomerService customerService, IToastNotification toastNotification)
        {
            _customerService = customerService;
            _toastNotification = toastNotification;
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


        public List<SelectListItem> Countries { get; set; }


        public void OnGet()
        {
            Birthday = DateTime.Now.AddYears(-20);
            SetCountries();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var cust = new Customer()
                {
                    Givenname = Givenname,
                    Surname = Surname,
                    Country = Country,
                    Telephone = Telephone,
                    EmailAddress = EmailAddress,
                    City = City,
                    Streetaddress = Adress,
                    NationalId = NationalId,
                    Birthday = Birthday,
                    Zipcode = Zipcode
                };
                var result = _customerService.CreateNewCustomer(cust);

                if(result == ICustomerService.ReturnCode.InvalidCountry)
                    ModelState.AddModelError(Country, "Valt land är inte giltigt");

                if (ModelState.IsValid)
                {
                    _toastNotification.AddSuccessToastMessage("Kunden har skapats");
                    return RedirectToPage("Customers");
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
