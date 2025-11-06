using E_Commerce.Data.Context;
using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public readonly E_commenceContext _context;

        public ProductoRepository(E_commenceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OperationResult<List<Producto>>> FilterAsync(FiltroDeProductos filter)
        {
            try
            {
                var query = Entity
                    .Include(p => p.Categoria)
                    .AsQueryable();

                if (filter.CategoriaId.HasValue)
                {
                    query = query.Where(p => p.CategoriaId == filter.CategoriaId.Value);
                }

                if (filter.PrecioMin.HasValue)
                {
                    query = query.Where(p => p.Precio >= filter.PrecioMin.Value);
                }

                if (filter.PrecioMax.HasValue)
                {
                    query = query.Where(p => p.Precio <= filter.PrecioMax.Value);
                }

                if (!string.IsNullOrEmpty(filter.Marca))
                {
                    query = query.Where(p => p.Marca == filter.Marca);
                }

                if (filter.EsNuevo.HasValue)
                {
                    query = query.Where(p => p.EsNuevo == filter.EsNuevo.Value);
                }

                if (filter.EsPopular.HasValue)
                {
                    query = query.Where(p => p.EsPopular == filter.EsPopular.Value);
                }

                if (!string.IsNullOrEmpty(filter.SearchTerm))
                {
                    query = query.Where(p => p.Nombre.Contains(filter.SearchTerm)
                    || p.Descripcion.Contains(filter.SearchTerm) ||
                    (p.Marca != null && p.Marca.Contains(filter.SearchTerm)));
                }

                var resultados = await query.ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = resultados,
                    Message = $"Se encontraron {resultados.Count} productos"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al filtrar productos: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetByCategoriesAsync(int categoriaID)
        {
            try
            {
                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.CategoriaId == categoriaID && p.Stock > 0)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se encontraron {productos.Count} productos en la categoría"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos por categoría: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetByCategoryByPopularAsync(int categoriaId, bool IsPopular)
        {
            try
            {
                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.EsPopular == IsPopular && p.CategoriaId == categoriaId && p.Stock > 0)
                    .OrderByDescending(p => p.EsPopular)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se encontraron {productos.Count} productos en la categoría"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos por categoría y popularidad: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetByMultiBrandsAsync(List<string> brands)
        {
            try
            {
                if (brands == null || !brands.Any())
                {
                    return new OperationResult<List<Producto>>
                    {
                        Success = false,
                        Message = "La lista de marcas no puede estar vacía",
                    };
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => brands.Contains(p.Marca) && p.Stock > 0)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se encontraron {productos.Count} productos de las marcas especificadas"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos por múltiples marcas: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetByPrecioRangeAsync(decimal minPrice, decimal maxPrice)
        {
            try
            {
                if (minPrice > maxPrice)
                {
                    return new OperationResult<List<Producto>>
                    {
                        Success = false,
                        Message = "El precio mínimo no puede ser mayor al precio máximo",
                    };
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.Precio >= minPrice && p.Precio <= maxPrice && p.Stock > 0)
                    .OrderBy(p => p.Precio)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se encontraron {productos.Count} productos en el rango de precio"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos por rango de precio: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetNewAsync(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    return new OperationResult<List<Producto>>
                    {
                        Success = false,
                        Message = "La cantidad debe ser mayor a cero",
                    };
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.EsNuevo && p.Stock > 0)
                    .OrderByDescending(p => p.Id)
                    .Take(cantidad)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se obtuvieron {productos.Count} productos nuevos"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos nuevos: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetPopularAsync(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    return new OperationResult<List<Producto>>
                    {
                        Success = false,
                        Message = "La cantidad debe ser mayor a cero",
                    };
                }

                var productos = await Entity
                   .Include(p => p.Categoria)
                   .Where(p => p.EsPopular && p.Stock > 0)
                   .OrderByDescending(p => p.EsPopular)
                   .Take(cantidad)
                   .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se obtuvieron {productos.Count} productos populares"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos populares: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetProductByBrand(string brand)
        {
            try
            {
                if (string.IsNullOrEmpty(brand))
                {
                    return new OperationResult<List<Producto>>
                    {
                        Success = false,
                        Message = "La marca no puede estar vacía",
                    };
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.Marca == brand && p.Stock > 0)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se encontraron {productos.Count} productos de la marca {brand}"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos por marca: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> GetProductByCategory()
        {
            try
            {
                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.Stock > 0)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se obtuvieron {productos.Count} productos con categoría"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error al obtener productos con categoría: {ex.Message}",
                };
            }
        }

        public async Task<OperationResult<List<Producto>>> SearchAsync(string search)
        {
            OperationResult<List<Producto>> result = new();
            try
            {
                if (string.IsNullOrWhiteSpace(search))
                {
                    return await GetProductByCategory();
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p =>
                            p.Nombre.Contains(search) ||
                            p.Descripcion.Contains(search) ||
                            (p.Marca != null && p.Marca.Contains(search)) ||
                            (p.Categoria != null && p.Categoria.Nombre.Contains(search)))
                    .Where(p => p.Stock > 0)
                    .ToListAsync();

                return new OperationResult<List<Producto>>
                {
                    Success = true,
                    Result = productos,
                    Message = $"Se encontraron {productos.Count} productos para '{search}'"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Producto>>
                {
                    Success = false,
                    Message = $"Error en la búsqueda: {ex.Message}",

                };
            }
        }
    }
} 
                