using BankStartWeb.Data;

namespace Bank_AB.Pages.Transactions
{
    public class TransactionVerification
    {
        public static bool CheckForOvercharge(decimal amount, decimal balance)
        {
            return balance - amount <= 0;
        }
    }
}
