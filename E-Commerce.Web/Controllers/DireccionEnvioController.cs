using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class DireccionEnvioController : Controller
    {
        private readonly IDireccionEnvioServices _services;
        public DireccionEnvioController( IDireccionEnvioServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var listaDireccionesDto = await _services.GetAllListDto();

            var listaDireccionesViewModel = listaDireccionesDto.Select(d => new DireccionEnvioViewModel
            {
                Id = d.Id,
                Calle = d.Calle,
                Ciudad = d.Ciudad,
                CodigoPostal = d.CodigoPostal,
                Pais = d.Pais,
                PedidoId = d.PedidoId
            }).ToList();

            return View(listaDireccionesViewModel);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View("Crear", new DireccionEnvioViewModel() { Calle = "", Ciudad = "", CodigoPostal = "", Pais = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DireccionEnvioViewModel save)
        {
            if (!ModelState.IsValid)
            {
                return View("Crear", save);
            }
            var direccionDto = new DireccionEnvioDto
            {
                Calle = save.Calle,
                Ciudad = save.Ciudad,
                CodigoPostal = save.CodigoPostal,
                Pais = save.Pais,
                PedidoId = save.PedidoId ?? 0
            };
            await _services.SaveDtoAsync(direccionDto);
            return RedirectToAction("Index");
        }
    }
}
