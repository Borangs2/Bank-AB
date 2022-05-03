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

    }

    [TestMethod]
    public void Deposit_amount_cannot_be_to_large()
    {

    }

    /*
     * --------
     * WITHDRAW
     * --------
     */

    [TestMethod]
    public void Withdraw_amount_cannot_be_negative()
    {

    }

    [TestMethod]
    public void Withdraw_amount_cannot_be_to_large()
    {

    }

    [TestMethod]
    public void Cannot_withdraw_more_then_on_account()
    {

    }

    /*
     * --------
     * TRANSFER
     * --------
     */

    [TestMethod]
    public void Transfer_amount_cannot_be_negative()
    {

    }

    [TestMethod]
    public void Cannot_Transfer_more_then_on_account()
    {

    }

    [TestMethod]
    public void Cannot_Transfer_to_unexisting_account()
    {

    }

    [TestMethod]
    public void Transfer_amount_cannot_be_to_large()
    {

    }
}