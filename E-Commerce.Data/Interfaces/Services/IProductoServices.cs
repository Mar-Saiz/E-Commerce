using E_Commerce.Data.Core;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Services
{
    public interface IProductoServices : IBaseServices<Producto, ProductoDto>
    {
        Task<OperationResult<List<Producto>>> FilterProductsAsync(FiltroDeProductos filtro);
        Task<OperationResult<List<Producto>>> GetProddcutsByCategory(int categoriaId);
        Task<OperationResult<List<Producto>>> GetProductsByPopularAndCategoryIDAsync(int categoriaId, bool esPopular = true);
        Task<OperationResult<List<Producto>>> GetProductByBrandAysnc(List<string> marcas);
        Task<OperationResult<List<Producto>>> GetByPrecioRangeAsync(decimal precioMin, decimal precioMax);
        Task<OperationResult<List<Producto>>> GetNewProductAsync(int cantidad);
        Task<OperationResult<List<Producto>>> GetPopulaarProductsAsync(int cantidad);
        Task<OperationResult<List<Producto>>> ObtenerProductosPorMarcaAsync(string marca);
        Task<OperationResult<List<Producto>>> GetAllProductAsnyc();
        Task<OperationResult<List<Producto>>> SearchProductAsync(string terminoBusqueda);

    }
}
