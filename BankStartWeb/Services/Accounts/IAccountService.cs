using Bank_AB.Data;

namespace Bank_AB.Services.Accounts;

public interface IAccountService
{
    Account? GetAccountFromId(int id);
}