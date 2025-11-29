using AutoMapper;
using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class PagosServices : BaseServices<Pagos, PagosDto>, IPagosServices
    {
        private readonly IPagosRepository _pagosRepository;
        private readonly IMapper _mapper;


        public PagosServices(IMapper mapper, IBaseRepository<Pagos> _baseRepository,  IPagosRepository pagosRepository) : base(mapper, _baseRepository)
        {
            _pagosRepository = pagosRepository;
            _mapper = mapper;
        }

        //validate payment details

        public bool ValidatePaymentDetails(PagosDto pagosDto)
        {
            if (string.IsNullOrWhiteSpace(pagosDto.MetodoPago) || string.IsNullOrWhiteSpace(pagosDto.UserId))
            {
                return false;
            }

            if (pagosDto.Monto <= 0 || pagosDto.PedidoId <= 0)
            {
                return false;
            }
            return true;
        }

        //Seleccionar metodo de pago
        public Task<OperationResult<PagosDto>> SelectPaymentMethod(int metodoSeleccionado, PagosDto pagosDto)
        {
            pagosDto .MetodoPago = metodoSeleccionado switch
            {
                0 => "Tarjeta",
                1 => "PayPal",
                2 => "Transferencia Bancaria",
                _ => "Método Desconocido"
            };

            if(pagosDto.MetodoPago == "Método Desconocido")
            {
                return Task.FromResult(new OperationResult<PagosDto>
                {
                    Success = false,
                    Message = "Método de pago inválido."
                });
            }

            if(!ValidatePaymentDetails(pagosDto))
            {
                return Task.FromResult(new OperationResult<PagosDto>
                {
                    Success = false,
                    Message = "Detalles de pago inválidos."
                });
            }

            return Task.FromResult(new OperationResult<PagosDto>
            {
                Success = true,
                Result = pagosDto
            });
        }

        //Confirmar Compra
        public async Task<OperationResult<PagosDto>> ConfirmPurchase(PagosDto pagosDto)
        {
            var result = new OperationResult<PagosDto>();

            if (!ValidatePaymentDetails(pagosDto))
            {
               result.Success = false;
               result.Message = "Detalles de pago inválidos.";

                return result;
            }

            var pagosEntity = _mapper.Map<Pagos>(pagosDto);

            await _pagosRepository.SaveEntityAsync(pagosEntity);

            result.Success = true;
            result.Result = pagosDto;

            return result;
        }

    }
}
