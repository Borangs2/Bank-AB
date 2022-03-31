using BankStartWeb.Data;

namespace Bank_AB.Services.Sort
{
    public class CustomerSortService : ISortService<Customer>
    {
        public IQueryable<Customer> Sort(IQueryable<Customer> query, string operation, string order)
        {
            switch (operation)
            {
                case "givenName":
                    if(order == "asc")
                        query = query.OrderBy(ord => ord.Givenname);
                    query = query.OrderByDescending(ord => ord.Givenname);
                    break;

                case "surname":
                    if (order == "asc")
                        query = query.OrderBy(ord => ord.Surname);
                    query = query.OrderByDescending(ord => ord.Surname);
                    break;

                case "country":
                    if (order == "asc")
                        query = query.OrderBy(ord => ord.Country);
                    query = query.OrderByDescending(ord => ord.Country);
                    break;

                case "city":
                    if (order == "asc")
                        query = query.OrderBy(ord => ord.City);
                    query = query.OrderByDescending(ord => ord.City);
                    break;

                case "telnum":
                    if (order == "asc")
                        query = query.OrderBy(ord => ord.Telephone);
                    query = query.OrderByDescending(ord => ord.Telephone);
                    break;

                case "email":
                    if (order == "asc")
                        query = query.OrderBy(ord => ord.EmailAddress);
                    query = query.OrderByDescending(ord => ord.EmailAddress);
                    break;

            }
            return query;
        }
    }
}
