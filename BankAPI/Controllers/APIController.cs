using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Bank_AB.Data;
using Bank_AB.Services.Customers;
using Bank_AB.Services.Accounts;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly AccountService _accountService;

        public APIController( CustomerService customerService, AccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }


        



    }
}