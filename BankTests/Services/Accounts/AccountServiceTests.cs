using Bank_AB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank_AB.Services.Accounts;

namespace BankTests.Services.AccountServiceTests
{
    [TestClass]
    internal class AccountServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Bank_AB")
                .Options;
            _context = new ApplicationDbContext(options);

            _accountService = new AccountService(_context);
        }



        /*
         * -------
         * GENERAL
         * -------
         */

        [TestMethod]
        public void Get_account_from_id()
        {

        }

        public void Get_account_from_invalid_id()
        {

        }



    }
}
