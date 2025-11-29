using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaServices _categories;

        public CategoriaController(ICategoriaServices categories)
        {
            _categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await _categories.GetAllListDto();

            var listEntity = dto.Select(s =>
            new CategoriaViewModel()
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion
               
            }).ToList();

            return View(listEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Categoria = await _categories.GetAllListDto();

            return View("Crear", new CategoriaViewModel() { Nombre = "", Descripcion = "", });
        }


        [HttpPost]
        public async Task<IActionResult> Crear(SaveProductosViewModel save)
        {
            ViewBag.Categoria = await _categories.GetAllListDto();

            if (ModelState.IsValid)
            {
                CategoriaDto dto = new()
                {
                    Id = 0,
                    Nombre = save.Nombre,
                    Descripcion = save.Descripcion,
                  
                };
                await _categories.SaveDtoAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View("Crear", save);
        }
    }
}
