using Bank_AB.Services;
using Bank_AB.Services.Transactions;
using BankStartWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTests.Services.Transactions
{
    public class FakeAccountService : IAccountService
    {
        public bool GetFlag = false;
        public Account GetAccountFromId(int id)
        {
            GetFlag = true;
            return new Account();
        }
    }




    [TestClass]
    internal class TransactionsServiceTests
    {
        private readonly TransactionsService _sut; //System under test
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountService;

        public TransactionsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Bank_AB")
                .Options;
            _context = new ApplicationDbContext(options);

            _accountService = new AccountService(_context);

            _sut = new TransactionsService(_context, _accountService);

        }





    }
}
