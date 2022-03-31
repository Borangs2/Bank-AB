namespace Bank_AB.Services.Sort
{
    public interface ISortService<T>
    {
        IQueryable<T> Sort(IQueryable<T> query, string operation, string order);
    }
}
