using System.Linq.Expressions;

namespace E_Commerce.Data.Interfaces.Services
{
    public interface IBaseServices<TEntity, TDto>
     where TEntity : class
     where TDto : class
    {

        Task<List<TDto>> GetAllListDto();
        Task<TDto?> GetDtoById(int id);
        Task<TDto?> UpdateDtoAsync(TDto dtoUpdate, int id);
        Task<TDto?> SaveDtoAsync(TDto dtoSave);
        Task<List<TDto>?> SaveRange(List<TDto> dtosToSave);
        Task<List<TDto>> GetWithInclude(List<string> properties);
        Task<bool> DeleteHardDtoAsync(int dtoDelete);
        Task<List<TDto>> GetAllFilteredAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                             List<string>? includes = null);

    }
}



