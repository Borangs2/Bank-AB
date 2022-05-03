using Bank_AB.Data;
using Bank_AB.Services.Accounts;
using Bank_AB.Services.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTests.Services.Transactions;

public class FakeAccountService : IAccountService
{
    public bool GetFlag;

    public Account GetAccountFromId(int id)
    {
        GetFlag = true;
        return new Account();
    }
}

[TestClass]
internal class TransactionsServiceTests
{
    private readonly AccountService _accountService;
    private readonly ApplicationDbContext _context;
    private readonly TransactionsService _sut; //System under test

    public TransactionsServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Bank_AB")
            .Options;
        _context = new ApplicationDbContext(options);

        _accountService = new AccountService(_context);

        _sut = new TransactionsService(_context, _accountService);
    }


    /*
     * -------
     * DEPOSIT
     * -------
     */


    [TestMethod]
    public void Deposit_amount_cannot_be_negative()
    {
        var result = _sut.Deposit(1, -50, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.ValueNegative);
    }

    [TestMethod]
    public void Deposit_amount_cannot_be_to_large()
    {
        var result = _sut.Deposit(1, long.MaxValue, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.ValueToHigh);
    }

    [TestMethod]
    public void Cannot_deposit_into_not_found_account()
    {
        var result = _sut.Deposit(0, 10, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.NotFound);
    }

    [TestMethod]
    public void Deposit_into_account_Ok()
    {
        var result = _sut.Deposit(1, 10, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.Ok);
    }

    /*
     * --------
     * WITHDRAW
     * --------
     */

    [TestMethod]
    public void Withdraw_amount_cannot_be_negative()
    {
        var result = _sut.Withdraw(1, 50, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.ValueToHigh);
    }

    [TestMethod]
    public void Withdraw_amount_cannot_be_to_large()
    {
        var result = _sut.Deposit(1, long.MaxValue, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.ValueToHigh);
    }

    [TestMethod]
    public void Cannot_withdraw_more_then_on_account()
    {
        var balance = _accountService.GetAccountFromId(1).Balance;
        var result = _sut.Withdraw(1, balance + 1000, "", "");
        Assert.AreEqual(result, ITransactionsService.ReturnCode.BalanceToLow);
    }

    /*
     * --------
     * TRANSFER
     * --------
     */

    [TestMethod]
    public void Transfer_amount_cannot_be_negative()
    {
        var result = _sut.Transfer(1, 2, -50);
        Assert.AreEqual(result, ITransactionsService.ReturnCode.ValueNegative);
    }

    [TestMethod]
    public void Transfer_amount_cannot_be_to_large()
    {
        var result = _sut.Transfer(1, 2, long.MaxValue);
        Assert.AreEqual(result, ITransactionsService.ReturnCode.ValueToHigh);
    }

    [TestMethod]
    public void Cannot_Transfer_more_then_on_account()
    {
        var balance = _accountService.GetAccountFromId(1).Balance;
        var result = _sut.Transfer(1, 2, balance);
        Assert.AreEqual(result, ITransactionsService.ReturnCode.BalanceToLow);
    }

    [TestMethod]
    public void Cannot_Transfer_to_unexisting_account()
    {
        var result = _sut.Transfer(1, -1, 10);
        Assert.AreEqual(result, ITransactionsService.ReturnCode.NotFound);
    }
}