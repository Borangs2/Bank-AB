using Bank_AB.Data;
using Bank_AB.Services.Customers;
using Microsoft.EntityFrameworkCore;

namespace Bank_AB.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly CustomerService _customerService;

    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Account? GetAccountFromId(int id)
    {
        
         var i = _context.Accounts.Include(trans => trans.Transactions).FirstOrDefault(acc => acc.Id == id);
         return i;
    }
    
    
}