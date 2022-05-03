using Bank_AB.Data;
using Bank_AB.Services.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTests.Services.Accounts;

[TestClass]
internal class AccountServiceTests
{
    private readonly AccountService _sut;
    private readonly ApplicationDbContext _context;

    public AccountServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Bank_AB")
            .Options;
        _context = new ApplicationDbContext(options);

        _sut = new AccountService(_context);
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

    [TestMethod]
    public void Get_account_from_invalid_id()
    {
    }
}