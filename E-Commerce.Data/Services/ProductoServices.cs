using AutoMapper;
using E_Commerce.Data.Core;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class ProductoServices : BaseServices<Producto, ProductoDto>, IProductoServices
    {
        public readonly IProductoRepository _productoRepository;
        public readonly IMapper _mapper;
        public ProductoServices(IProductoRepository productoRepository, IMapper mapper) : base(mapper, productoRepository)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;
        }

        public async Task<OperationResult<List<Producto>>> FilterProductsAsync(FiltroDeProductos filtro)
        {
            if (filtro == null)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "El filtro no puede ser nulo"
                };
            }

            return await _productoRepository.FilterAsync(filtro);
        }

        public async Task<OperationResult<List<Producto>>> GetProddcutsByCategory(int categoriaId)
        {
            if (categoriaId <= 0)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "El ID de categoría debe ser mayor a cero"
                };
            }

            return await _productoRepository.GetByCategoriesAsync(categoriaId);
        }

        public async Task<OperationResult<List<Producto>>> GetProductsByPopularAndCategoryIDAsync(int categoriaId, bool esPopular = true)
        {
            if (categoriaId <= 0)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "El ID de categoría debe ser mayor a cero"
                };
            }

            return await _productoRepository.GetByCategoryByPopularAsync(categoriaId, esPopular);
        }

        public async Task<OperationResult<List<Producto>>> GetProductByBrandAysnc(List<string> marcas)
        {
            if (marcas == null || !marcas.Any())
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La lista de marcas no puede estar vacía"
                };
            }

            if (marcas.Any(string.IsNullOrWhiteSpace))
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La lista de marcas contiene elementos vacíos o nulos"
                };
            }

            return await _productoRepository.GetByMultiBrandsAsync(marcas);
        }

        public async Task<OperationResult<List<Producto>>> GetByPrecioRangeAsync(decimal precioMin, decimal precioMax)
        {
            if (precioMin < 0 || precioMax < 0)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "Los precios no pueden ser negativos"
                };
            }

            if (precioMin > precioMax)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "El precio mínimo no puede ser mayor al precio máximo"
                };
            }

            return await _productoRepository.GetByPrecioRangeAsync(precioMin, precioMax);
        }

        public async Task<OperationResult<List<Producto>>> GetNewProductAsync(int cantidad)
        {
            if (cantidad <= 0)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La cantidad debe ser mayor a cero"
                };
            }

            if (cantidad > 100)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La cantidad no puede ser mayor a 100"
                };
            }

            return await _productoRepository.GetNewAsync(cantidad);
        }

        public async Task<OperationResult<List<Producto>>> GetPopulaarProductsAsync(int cantidad)
        {
            if (cantidad <= 0)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La cantidad debe ser mayor a cero"
                };
            }

            if (cantidad > 100)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La cantidad no puede ser mayor a 100"
                };
            }

            return await _productoRepository.GetPopularAsync(cantidad);
        }

        public async Task<OperationResult<List<Producto>>> ObtenerProductosPorMarcaAsync(string marca)
        {
            if (string.IsNullOrWhiteSpace(marca))
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "La marca no puede estar vacía"
                };
            }

            return await _productoRepository.GetProductByBrand(marca);
        }

        public async Task<OperationResult<List<Producto>>> GetAllProductAsnyc()
        {
            return await _productoRepository.GetProductByCategory();
        }

        public async Task<OperationResult<List<Producto>>> SearchProductAsync(string terminoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(terminoBusqueda))
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "El término de búsqueda no puede estar vacío"
                };
            }

            if (terminoBusqueda.Length < 3)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = "El término de búsqueda debe tener al menos 3 caracteres"
                };
            }

            return await _productoRepository.SearchAsync(terminoBusqueda);
        }
    }
}

