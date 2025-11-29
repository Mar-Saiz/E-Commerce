using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoServices _services;
        private readonly ICategoriaServices _categories;

        public ProductoController(IProductoServices productoServices, ICategoriaServices categories)
        {
            _services = productoServices;
            _categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await _services.GetAllListDto();

            var listEntity = dto.Select(s =>
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

            return View(listEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Categoria = await _categories.GetAllListDto();

            return View("Crear", new SaveProductosViewModel() { Nombre = "", Descripcion = "", Stock = 0, EsNuevo = true, EsPopular = false, Marca = "", Precio = 0, CategoriaId = 0 });
        }

        [HttpPost]
        public async Task<IActionResult> Crear(SaveProductosViewModel save)
        {
            ViewBag.Categoria = await _categories.GetAllListDto();

            if (ModelState.IsValid)
            {
                ProductoDto dto = new() 
                { Id = 0, Nombre = save.Nombre,
                  Descripcion = save.Descripcion,
                  Stock = save.Stock,
                  EsNuevo = save.EsNuevo,
                  EsPopular = save.EsPopular,
                  Marca = save.Marca,
                  Precio = save.Precio,
                  CategoriaId = save.CategoriaId 
                };
                await _services.SaveDtoAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View("Crear", save);
        }
    }
}