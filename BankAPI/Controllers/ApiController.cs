using AutoMapper;
using Bank_AB.Data;
using Microsoft.AspNetCore.Mvc;
using Bank_AB.Services.Customers;
using Bank_AB.Services.Accounts;
using Bank_AB.ViewModels;
 

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ApiController(ICustomerService customerService, IAccountService accountService, IMapper mapper)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("me/{id}")]
        public IActionResult GetCustomerFromId(int id)
        {
            var t = _customerService.GetCustomerFromId(id);
            var cust = _mapper.Map(t, new CustomerViewModel());

            var accounts = _customerService.GetAccounts(id).Select(a => _mapper.Map(a, new AccountViewModel())).ToList();

            var tempTuple = new Tuple<CustomerViewModel, List<AccountViewModel>>(cust, accounts);


            return Ok(_mapper.Map(tempTuple, new ApiCustomerViewModel()));
        }

        [HttpGet]
        [Route("accounts/{id}/{numTransactions}/{startAt}")]
        public IActionResult GetTransactionsFromAccountId(int id, int numTransactions, int startAt)
        {
            var acc = _accountService.GetAccountFromId(id);

            return Ok(acc.Transactions.ToList().Select(t => _mapper.Map(t, new TransactionViewModel()))
                .Skip(startAt).Take(numTransactions)
                .ToList()
            );
        }
    }
}