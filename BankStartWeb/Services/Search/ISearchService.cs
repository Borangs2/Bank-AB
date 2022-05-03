namespace Bank_AB.Services.Search;

public interface ISearchService<T>
{
    public IQueryable<T> Search(IQueryable<T> query, string searchTerm);
}