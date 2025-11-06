using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Repository
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task<OperationResult<List<Producto>>> GetByCategoriesAsync(int categoriaID);
        Task<OperationResult<List<Producto>>> GetByCategoryByPopularAsync(int categoriaId, bool IsPopular);
        Task<OperationResult<List<Producto>>> FilterAsync(FiltroDeProductos filter);
        Task<OperationResult<List<Producto>>> GetByPrecioRangeAsync(decimal minPrice, decimal maxPrince);
        Task<OperationResult<List<Producto>>> SearchAsync(string search);
        Task<OperationResult<List<Producto>>> GetByMultiBrandsAsync(List<string> brand);
        Task<OperationResult<List<Producto>>> GetPopularAsync(int cantidad);
        Task<OperationResult<List<Producto>>> GetProductByCategory();
        Task<OperationResult<List<Producto>>> GetProductByBrand(string brand);
        Task<OperationResult<List<Producto>>> GetNewAsync(int cantidad);
    }
}
