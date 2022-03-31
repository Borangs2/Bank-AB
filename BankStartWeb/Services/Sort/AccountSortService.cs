using BankStartWeb.Data;

namespace Bank_AB.Services.Sort
{
    public class AccountSortService : ISortService<Account>
    {
        public IQueryable<Account> Sort(IQueryable<Account> query, string operation, string order)
        {
            throw new NotImplementedException();
        }
    }
}
