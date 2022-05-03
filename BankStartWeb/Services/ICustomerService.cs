using Bank_AB.Data;

namespace Bank_AB.Services;

public interface ICustomerService
{
    Customer GetCustomerFromId(int id);
}