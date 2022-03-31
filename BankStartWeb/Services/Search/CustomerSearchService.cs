using BankStartWeb.Data;

namespace Bank_AB.Services
{
    public class CustomerSearchService : ISearchService<Customer>
    {
        public IQueryable Search(IQueryable<Customer> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();

                return query.Where(ord => 
                ord.Givenname.ToLower().Contains(searchTerm) ||
                ord.Surname.ToLower().Contains(searchTerm)   
                ).AsQueryable();
            }
            return query;
        }
    }
}
