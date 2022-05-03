using System;
using Bank_AB.Data;
using Bank_AB.Services.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTests.Services.Account;

[TestClass]
public class AccountServiceTests
{
    private readonly AccountService _sut; //system under test
    private readonly ApplicationDbContext _context;

    public AccountServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Bank_AB")
            .Options;
        _context = new ApplicationDbContext(options);

        _sut = new AccountService(_context);

        var data = new TestDataInitilizer(_context);
        data.SeedData();
    }




    [TestMethod]
    public void Get_account_from_id()
    {
        var result = _sut.GetAccountFromId(1);
        Assert.AreNotEqual(result, null);
    }

    [TestMethod]
    public void Get_account_from_invalid_id()
    {
        var result = _sut.GetAccountFromId(-4);
        Assert.AreEqual(result, null);
    }
}