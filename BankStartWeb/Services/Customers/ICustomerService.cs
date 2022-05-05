using Bank_AB.Data;

namespace Bank_AB.Services.Customers;

public interface ICustomerService
{
    Customer GetCustomerFromId(int id);
    List<Account> GetAccounts(int id);
}