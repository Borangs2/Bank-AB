using BankStartWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank_AB.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Account GetAccountFromId(int id)
        {
            return _context.Accounts.Include(trans => trans.Transactions).FirstOrDefault(acc => acc.Id == id);
        }

    }
}
