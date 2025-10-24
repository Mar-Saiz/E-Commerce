using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class CuponServices : BaseServices<Cupon, CuponDto>, ICuponServices
    {
        public readonly ICuponRepository _categoriaRepository;
        public readonly IMapper _mapper;
        public CuponServices(ICuponRepository cuponRepository, IMapper mapper) : base(mapper, cuponRepository)
        {
            _mapper = mapper;
            _categoriaRepository = cuponRepository;
        }

        // validar cupon.

        public async Task<CuponDto> ValidarCuponAsync(CuponDto cuponDto)
        {
            var cupon = await _categoriaRepository.GetEntityByIdAsync(cuponDto.Id);
            if (cupon == null || cupon.FechaExpiracion < DateTime.Now)
            {
                return null; // Cupón inválido o expirado
            }
            return _mapper.Map<CuponDto>(cupon);
        }


        // aplicar cupon al carrito

        public async Task<decimal> AplicarCuponAlCarritoAsync(CuponDto cuponDto, decimal totalCarrito)
        {
            var cupon = await ValidarCuponAsync(cuponDto);
            if (cupon == null)
            {
                return 0;
            }
            decimal descuento = (totalCarrito * cupon.Descuento);

            return totalCarrito - descuento;
        }


    }
}
