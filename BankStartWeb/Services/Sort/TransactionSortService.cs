using System.Transactions;

namespace Bank_AB.Services.Sort
{
    public class TransactionSortService : ISortService<Transaction>
    {
        public IQueryable<Transaction> Sort(IQueryable<Transaction> query, string operation, string order)
        {
            throw new NotImplementedException();
        }
    }
}
