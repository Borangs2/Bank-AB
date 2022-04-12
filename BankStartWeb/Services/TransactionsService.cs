namespace Bank_AB.Services
{
    public class TransactionsService : ITransactionsService
    {
        public ITransactionsService.ReturnCode Deposit(int accountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public ITransactionsService.ReturnCode Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public ITransactionsService.ReturnCode Withdraw(int accountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
