using Bank_AB.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace Bank_AB.Pages.Customers
{
    public class AddNewAccountModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IToastNotification _toastNotification;

        public AddNewAccountModel(ICustomerService customerService, IToastNotification toastNotification)
        {
            _customerService = customerService;
            _toastNotification = toastNotification;

        }

        public List<SelectListItem> AccountTypes { get; set; }
        public int CustomerId { get; set; }

        [BindProperty]
        public string AccountType { get; set; }

        public void OnGet(int id)
        {
            CustomerId = id;
            GetAccountTypes();
        }

        public IActionResult OnPost(int id)
        {
            var result = _customerService.CreateNewAccount(id, AccountType);

            if (result == ICustomerService.ReturnCode.InvalidId)
            {
                _toastNotification.AddErrorToastMessage("Ogiltigt Id");
                return Page();
            }

            _toastNotification.AddSuccessToastMessage("Kontot har skapats");
            return RedirectToPage("/Customers/CustomerDetails", new {id = id});
        }


        private void GetAccountTypes()
        {
            AccountTypes = new List<SelectListItem>
            {
                new SelectListItem{Text = "Sparkonto", Value = "Savings"},
                new SelectListItem{Text = "Checkkonto", Value = "Checking"},
                new SelectListItem{Text = "Personligt konto", Value = "Personal"},
            };


        }
    }
}
