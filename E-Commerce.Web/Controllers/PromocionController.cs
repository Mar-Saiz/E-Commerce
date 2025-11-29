using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class PromocionController : Controller
    {
        private readonly IPromocionServices _services;
        public PromocionController(IPromocionServices service)
        {
            _services = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var dto = await _services.GetAllListDto();

            var listEntity = dto.Select(s =>
            new PromocionViewModel()
            {
                Id = s.Id,
                Titulo = s.Titulo,
                Descripcion = s.Descripcion,
                ImagenUrl = s.ImagenUrl
            }).ToList();

            return View(listEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            return View("Crear", new SavePromocionViewModel()
            {
                Id = 0,
                Titulo = "",
                Descripcion = "",
                ImagenUrl = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> Crear(SavePromocionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                PromocionDto dto = new()
                {
                    Id = vm.Id,
                    Titulo = vm.Titulo,
                    Descripcion = vm.Descripcion,
                    ImagenUrl = vm.ImagenUrl
                };
                await _services.SaveDtoAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View("Crear", vm);
        }


    }
}
