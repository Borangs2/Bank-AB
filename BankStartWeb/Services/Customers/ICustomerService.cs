using Bank_AB.Data;
using Bank_AB.ViewModels;

namespace Bank_AB.Services.Customers;

public interface ICustomerService
{
    public enum ReturnCode
    {
        Ok,
        InvalidCountry,
        InvalidId,
    }
    Customer? GetCustomerFromId(int id);
    List<Account> GetAccounts(int id);
    ReturnCode CreateNewCustomer(Customer customer);
    ReturnCode EditCustomer(Customer customer, int id);
    ReturnCode CreateNewAccount(int customerId, string accountType);

}