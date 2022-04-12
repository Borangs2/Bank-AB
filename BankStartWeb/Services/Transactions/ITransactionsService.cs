namespace Bank_AB.Services
{
    public interface ITransactionsService
    {
        public enum ReturnCode
        {
            Ok,
            ValueNegative,
            BalanceToLow,
            NotFound,
        }

        ReturnCode Withdraw(int accountId, decimal amount, string operation, string type);
        ReturnCode Deposit(int accountId, decimal amount, string operation, string type);
        ReturnCode Transfer(int fromAccountId, int toAccountId, decimal amount);




    }
}
