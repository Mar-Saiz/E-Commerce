using AutoMapper;
using E_Commerce.Data.Core;
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

        // Maria Abreu 2024-0003

        // validar cupon.

        public async Task<OperationResult<CuponDto>> ValidarCuponAsync(CuponDto cuponDto)
        {
            OperationResult<CuponDto> result = new();

            var cupon = await _categoriaRepository.GetEntityByIdAsync(cuponDto.Id);

            if (cupon == null || cupon.FechaExpiracion < DateTime.Now)
            {
                result.Success = false;
                result.Message = "Cupón inválido o expirado.";
                return result;
            }

            result.Result = _mapper.Map<CuponDto>(cupon);
            result.Success = true;
            return result;
        }


        // Buscar cupon

        public async Task<OperationResult<CuponDto>> GetCuponByCodeAsync(string code)
        {
            OperationResult<CuponDto> result = new();
            var cupon = await _categoriaRepository.(code);
            if (cupon == null)
            {
                result.Success = false;
                result.Message = "Cupón no encontrado.";
                return result;
            }
            result.Result = _mapper.Map<CuponDto>(cupon);
            result.Success = true;
            return result;
        }


    }
}
