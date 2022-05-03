namespace Bank_AB.Services.Transactions;

public interface ITransactionsService
{
    public enum ReturnCode
    {
        Ok,
        ValueNegative,
        ValueToHigh,
        BalanceToLow,
        NotFound
    }

    ReturnCode Withdraw(int accountId, decimal amount, string operation, string type);
    ReturnCode Deposit(int accountId, decimal amount, string operation, string type);
    ReturnCode Transfer(int fromAccountId, int toAccountId, decimal amount);
}