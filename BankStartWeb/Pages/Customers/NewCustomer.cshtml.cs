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
    [Authorize(Roles = "Administrat�r, Kass�r")]
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
        [MaxLength(50, ErrorMessage = "Namnet �r �r f�r l�ngt")]
        [Required(ErrorMessage = "F�rnamn �r obligatoriskt")]
        public string Givenname { get; set; } = null!;
        [BindProperty]
        [MaxLength(50, ErrorMessage = "Namnet �r �r f�r l�ngt")]
        [Required(ErrorMessage = "Efternamn �r obligatoriskt")]
        public string Surname { get; set; } = null!;
        [BindProperty]
        [MaxLength(100, ErrorMessage = "Namnet �r f�r l�ngt")]
        [Required(ErrorMessage = "Land �r obligatoriskt")]
        public string Country { get; set; } = null!;
        [BindProperty]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "F�ltet m�ste inneh�lla ett telefonnummer")]
        public string Telephone { get; set; } = null!;
        [BindProperty]
        [DataType(DataType.EmailAddress, ErrorMessage = "F�ltet m�ste inneh�lla en mejladress")]
        [Required(ErrorMessage = "Mejladress �r obligatoriskt")]
        public string EmailAddress { get; set; } = null!;
        [BindProperty]
        [MaxLength(90, ErrorMessage = "Namnet �r f�r l�ngt")]
        [Required(ErrorMessage = "Stad �r obligatoriskt")]
        public string City { get; set; } = null!;
        [BindProperty]
        [MaxLength(60, ErrorMessage = "Namnet �r f�r l�ngt")]
        [Required(ErrorMessage = "Hemadress �r obligatoriskt")]
        public string Adress { get; set; } = null!;
        [BindProperty]
        [MaxLength(30, ErrorMessage = "Personnumret �r f�r l�ngt")]
        [Required(ErrorMessage = "Personnummer �r obligatoriskt")]
        public string NationalId { get; set; } = null!;
        [BindProperty]
        [MaxLength(10, ErrorMessage = "Postnumret �r f�r l�ngt")]
        [Required(ErrorMessage = "Postnummer �r obligatoriskt")]
        public string Zipcode { get; set; } = null!;
        [BindProperty]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "F�delsedatum �r obligatoriskt")]
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
                    ModelState.AddModelError(Country, "Valt land �r inte giltigt");

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
