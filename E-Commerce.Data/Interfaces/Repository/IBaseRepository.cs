namespace E_Commerce.Data.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetEntityByIdAsync(int Id);
        Task<T> SaveEntityAsync(T entity);
        Task<List<T>?> AddRangeAsync(List<T> entities);
        Task<T?> UpdateEntityAsync(int id, T entity);
        Task<List<T>> GetAllListAsync();
        IQueryable<T> GetAllQuery();
        Task DeleteAsync(int Id);
        Task<List<T>> GetAllListWithInclude(List<string> properties);
        IQueryable<T> GetAllQueryWithInclude(List<string> properties);
    }

}
