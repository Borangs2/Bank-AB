using BankStartWeb.Data;

namespace Bank_AB.Services.Search
{
    public class AccountSearchService : ISearchService<Account>
    {
        public IQueryable Search(IQueryable<Account> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();

                return query.Where(ord =>
                ord.Id.ToString().Contains(searchTerm) ||
                ord.AccountType.ToLower().Contains(searchTerm)
                ).AsQueryable();
            }
            return query;
        }
    }
}
