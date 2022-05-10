using Bank_AB.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank_AB.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Customer? GetCustomerFromId(int id)
    {
        return _context.Customers.Include(c => c.Accounts).FirstOrDefault(cust => cust.Id == id);
    }

    public List<Account> GetAccounts(int id)
    {
        return GetCustomerFromId(id).Accounts;
    }
}