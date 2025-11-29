using System.Runtime.Intrinsics.Arm;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class CuponController : Controller
    {
        private readonly ICuponServices _services;
        public CuponController(ICuponServices cuponServices)
        {
            _services = cuponServices;
      
        }
        public async Task<IActionResult> Index()
        {
            var dto = await _services.GetAllListDto();

            var listEntity = dto.Select(s =>
            new CuponViewModel()
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Descuento = s.Descuento,
                FechaExpiracion = s.FechaExpiracion
            }).ToList();

            return View(listEntity);
        }


        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            return View("Crear", new SaveCuponViewModel()
            {
                Id = 0,
                Codigo = "",
                Descuento = 0,
                FechaExpiracion = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Crear(SaveCuponViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CuponDto dto = new()
                {
                    Id = 0,
                    Codigo = vm.Codigo,
                    Descuento = vm.Descuento,
                    FechaExpiracion = vm.FechaExpiracion
                };
                await _services.SaveDtoAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View("Crear", vm);
        }

        
    }
}
