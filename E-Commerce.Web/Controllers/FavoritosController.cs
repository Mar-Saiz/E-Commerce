using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly IProductoServices _productoServices;

        public FavoritosController(IProductoServices productoServices)
        {
            _productoServices = productoServices;
        }

        public async Task<IActionResult> Index()
        {
            var favoriteProductsResult = await _productoServices.GetPopulaarProductsAsync(20); //first 20 products

            var list = favoriteProductsResult.Result?.Select(s =>
             new ProductoViewModel()
             {
                 Id = s.Id,
                 Nombre = s.Nombre,
                 Descripcion = s.Descripcion,
                 Stock = s.Stock,
                 EsNuevo = s.EsNuevo,
                 EsPopular = s.EsPopular,
                 Marca = s.Marca,
                 Precio = s.Precio,
                 CategoriaId = s.CategoriaId,
                 Categoria = s.Categoria == null ? null : new CategoriaViewModel()
                 {
                     Id = s.Id,
                     Nombre = s.Nombre,
                     Descripcion = s.Descripcion,
                 }

             }).ToList();

            return View(list);
        }
    }
}
