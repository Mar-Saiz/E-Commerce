using E_Commerce.Data.Context;
using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                    Message = $"Se encontraron {resultados.Count} productos."
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
            OperationResult<List<Producto>> result = new OperationResult<List<Producto>>();

            try
            {
                if (categoriaID < 0 )
                { 
                   result.Success = false;
                   result.Message = "El numero de categoria no puede ser menor a 0";
                   return result;
                }

                if (_context.Productos.Any(p => p.CategoriaId == categoriaID))
                {
                    result.Success = false;
                    result.Message = "El numero de categoria ya se encuentra registrado.";
                    return result;
                }

                    var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.CategoriaId == categoriaID && p.Stock > 0)
                    .ToListAsync();

                result.Success = true;
                result.Message = $"Se encontraron {productos.Count} productos en la categoría";
                return result;
            }
            catch (Exception)
            { 
                result.Success= false;
                result.Message = $"Error al obtener productos por categoría";
                return result;
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
            OperationResult<List<Producto>> result = new OperationResult<List<Producto>>();

            try
            {
                if (brands == null || !brands.Any())
                {
                    result.Success = false;
                    result.Message = "La lista de marcas no puede estar vacía";
                    return result;
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => brands.Contains(p.Marca) && p.Stock > 0)
                    .ToListAsync();


                result.Success = true;
                result.Message = $"Se encontraron {productos.Count} productos de las marcas especificadas";
                return result;
              
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener productos por múltiples marcas: {ex.Message}";
                return result;
            }
        }

        public async Task<OperationResult<List<Producto>>> GetByPrecioRangeAsync(decimal minPrice, decimal maxPrice)
        {
            OperationResult<List<Producto>> result = new OperationResult<List<Producto>>();

            try
            {
                if (minPrice > maxPrice)
                {
                    result.Success = false;
                    result.Message = "El precio mínimo no puede ser mayor al precio máximo";
                    return result;
                }

                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.Precio >= minPrice && p.Precio <= maxPrice && p.Stock > 0)
                    .OrderBy(p => p.Precio)
                    .ToListAsync();

                result.Success = true;
                result.Message = $"Se encontraron productos en el rango de precio";
                result.Result = productos;
                return result;
               
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener productos por rango de precio: {ex.Message}";
                return result;
            }
        }

        public async Task<OperationResult<List<Producto>>> GetNewAsync(int cantidad)
        {
            OperationResult<List<Producto>> result = new OperationResult<List<Producto>>();

            try
            {
                if (cantidad <= 0)
                {
                    result.Success = false;
                    result.Message = "La cantidad debe ser mayor a cero";
                    return result;
                  
                }
                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.EsNuevo && p.Stock > 0)
                    .OrderByDescending(p => p.Id)
                    .Take(cantidad)
                    .ToListAsync();

                result.Success = true;
                result.Message = $"Se obtuvieron {productos.Count} productos nuevos";
                result.Result = productos;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener productos nuevos: {ex.Message}";
                return result;
            }
        }

        public async Task<OperationResult<List<Producto>>> GetPopularAsync(int cantidad)
        {
            OperationResult<List<Producto>> result = new OperationResult<List<Producto>>();

            try
            {
                if (cantidad <= 0)
                {
                    result.Success = false;
                    result.Message = "La cantidad debe ser mayor a cero";
                    return result;
                }

                var productos = await Entity
                   .Include(p => p.Categoria)
                   .Where(p => p.EsPopular && p.Stock > 0)
                   .OrderByDescending(p => p.EsPopular)
                   .Take(cantidad)
                   .ToListAsync();

                result.Success = true;
                result.Result = productos;
                result.Message = $"Se obtuvieron {productos.Count} productos populares";
                return result;
               
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener productos populares: {ex.Message}";
                return result;
            }
        }

        public async Task<OperationResult<List<Producto>>> GetProductByBrand(string brand)
        {

            OperationResult<List<Producto>> result = new OperationResult<List<Producto>>();

            try
            {
                if (string.IsNullOrWhiteSpace(brand))
                {
                    result.Success = false;
                    result.Message = "La marca no puede estar vacía";
                    return result;
                }

                if (_context.Productos.Any(p => p.Marca == brand))
                {
                    result.Success = true;
                    result.Message = "La marca del producto ya esta registrado.";
                    return result;
                }   


                var productos = await Entity
                    .Include(p => p.Categoria)
                    .Where(p => p.Marca == brand && p.Stock > 0)
                    .ToListAsync();

                result.Success = true;
                result.Message = $"La marca del producto ya esta registrado.";
                result.Result = productos;
                return result;
               
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener productos por marca: {ex.Message}";
                return result;
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

                result.Success = true;
                result.Result = productos;
                result.Message = $"Se encontraron {productos.Count} productos para '{search}' ";
                return result;
            }
            catch (Exception ex)
            {
                result.Success= false;
                result.Message = $"Error en la búsqueda: {ex.Message}";
                return result ;
            }
        }

        public OperationResult<Producto> AddProduct(Producto? producto)
        { 
            OperationResult<Producto> result = new();

            if (producto is null)
            {
                result.Success = false;
                result.Message = "El objeto producto es requerido.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(producto!.Nombre))
            {
                result.Success = false;
                result.Message = "El nombre del producto es requerido";
                return result;
            }

            if (producto.Nombre.Length > 60)
            {
                result.Success = false;
                result.Message = "El nombre del producto sobrepasa el limite definido de 60";
                return result;
            }

            if (_context.Productos.Any(p => p.Nombre == producto.Nombre))
            {
                result.Success = false;
                result.Message = "El nombre del producto ya se encuentra registrado.";
                return result;
            }

            result.Success = true;
            result.Message = "Producto Agregado con exito";
            result.Result = producto;

            _context.Productos.Add(producto);
            _context.SaveChanges();

            return result;
        }

        public OperationResult<Producto> UpdateProduct(Producto? producto)
        {
            OperationResult<Producto> result = new();

            if (producto is null)
            {
                result.Success = false;
                result.Message = "El objeto producto es requerido.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(producto!.Nombre))
            {
                result.Success = false;
                result.Message = "El nombre del producto es requerido";
                return result;
            }

            if (producto.Nombre.Length > 60)
            {
                result.Success = false;
                result.Message = "El nombre del producto ya se encuentra registrado.";
                return result;
            }

            if (_context.Productos.Any(p => p.Nombre == producto.Nombre))
            {
                result.Success = false;
                result.Message = "El nombre del producto ya se encuentra registrado.";
                return result;
            }

            result.Success = true;
            result.Message = "Producto actualizado con exito";
            result.Result = producto;


            _context.Productos.Update(producto);
            _context.SaveChanges();

            return result;
        }
    }
} 
                