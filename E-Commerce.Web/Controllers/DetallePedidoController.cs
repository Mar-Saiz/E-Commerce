using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class DetallePedidoController : Controller
    {
        private readonly IDetallePedidoServices _detallePedidoServices;
        public DetallePedidoController(IDetallePedidoServices detallePedidoServices)
        {
            _detallePedidoServices = detallePedidoServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await _detallePedidoServices.GetAllListDto();

            var listEntity = dto.Select(s =>
            new DetallePedidoViewModel()
            {
                Id = s.Id,
                PedidoId = s.PedidoId,
                ProductoId = s.ProductoId,
                Cantidad = s.Cantidad,
                PrecioUnitario = s.PrecioUnitario


            }).ToList();

            return View(listEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Detalle = await _detallePedidoServices.GetAllListDto();

            return View("Crear", new DetallePedidoViewModel()
            { PedidoId = 0, ProductoId = 0, PrecioUnitario = 0, Cantidad = 0, });
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DetallePedidoViewModel save)
        {
            ViewBag.Detalle = await _detallePedidoServices.GetAllListDto();

            if (ModelState.IsValid)
            {
                DetallePedidoDto dto = new()
                {
                    Id = 0,
                    PedidoId = save.PedidoId,
                    ProductoId = save.ProductoId,
                    Cantidad = save.Cantidad,
                    PrecioUnitario = save.PrecioUnitario

                };
                await _detallePedidoServices.SaveDtoAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View("Crear", save);
        }
    }
}
