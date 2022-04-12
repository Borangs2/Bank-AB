using Bank_AB.Pages.Transactions;
using BankStartWeb.Data;
using System.Security.Principal;

namespace Bank_AB.Services
{
    public class TransactionsService : ITransactionsService
    {
        private ApplicationDbContext _context;
        private AccountService _accountService;
        public TransactionsService(ApplicationDbContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }


        public ITransactionsService.ReturnCode Deposit(int accountId, decimal amount, string operation, string type)
        {
            throw new NotImplementedException();
        }

        public ITransactionsService.ReturnCode Withdraw(int accountId, decimal amount, string operation, string type)
        {
            Account account = _accountService.GetAccountFromId(accountId);

            if (account == null)
                return ITransactionsService.ReturnCode.NotFound;

            if (TransactionVerification.CheckForOvercharge(amount, account.Balance))
                return ITransactionsService.ReturnCode.BalanceToLow;


            var transaction = new Transaction
            {
                Type = type,
                Operation = operation,
                Date = DateTime.Now,
                Amount = amount,
                NewBalance = account.Balance - amount
            };
            account.Balance -= amount;

            account.Transactions.Add(transaction);
            //_context.SaveChanges();

            return ITransactionsService.ReturnCode.Ok;

        }

        public ITransactionsService.ReturnCode Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}