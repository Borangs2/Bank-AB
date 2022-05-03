using Bank_AB.Data;

namespace Bank_AB.Services.Search;

public class TransactionSearchService : ISearchService<Transaction>
{
    public IQueryable<Transaction> Search(IQueryable<Transaction> query, string searchTerm)
    {
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower().Trim();

            return query.Where(ord =>
                ord.Id.ToString().Contains(searchTerm) ||
                ord.Type.ToLower().Contains(searchTerm) ||
                ord.Operation.ToLower().Contains(searchTerm) ||
                ord.Date.ToString().Contains(searchTerm)
            ).AsQueryable();
        }

        return query;
    }
}