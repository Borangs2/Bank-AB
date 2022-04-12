using BankStartWeb.Data;

namespace Bank_AB.Services
{
    public interface IAccountService
    {
        Account GetAccountFromId(int id);
    }
}
