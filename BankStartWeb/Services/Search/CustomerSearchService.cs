using BankStartWeb.Data;

namespace Bank_AB.Services.Search
{
    public class CustomerSearchService : ISearchService<Customer>
    {
        public IQueryable<Customer> Search(IQueryable<Customer> query, string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();

                return query.Where(ord => 
                ord.Givenname.ToLower().Contains(searchTerm) ||
                ord.Surname.ToLower().Contains(searchTerm)   ||
                ord.Country.ToLower().Contains(searchTerm)   ||
                ord.City.ToLower().Contains(searchTerm)
                ).AsQueryable();
            }
            return query;
        }
    }
}
