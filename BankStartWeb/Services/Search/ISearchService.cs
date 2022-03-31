namespace Bank_AB.Services
{
    public interface ISearchService<T>
    {
        public IQueryable Search(IQueryable<T> query, string searchTerm);
    }
}
