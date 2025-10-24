using AutoMapper;
using E_Commerce.Data.Core;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class PedidoServices : BaseServices<Pedido, PedidoDto>, IPedidoServices
    {
        public readonly IPedidoRepository _pedidoRepository;
        public readonly IAccountServiceForWebApp _accountServiceForWebApp;
        public readonly IMapper _mapper;

        public PedidoServices(IPedidoRepository pedidoRepository, IMapper mapper, IAccountServiceForWebApp accountServiceForWebApp) : base(mapper, pedidoRepository)
        {
            _mapper = mapper;
           _pedidoRepository = pedidoRepository;
            _accountServiceForWebApp = accountServiceForWebApp;
        }

        //
        // Jamil Guzman 2024-100 (Historial de pedidos por usuario)
        //
        public async Task<OperationResult<List<PedidoDto>>> GetOrderListByUserId(string userId)
        {
            OperationResult<List<PedidoDto>> result = new();

            //case 1: null or empty string

            if (string.IsNullOrWhiteSpace(userId)) 
            {
                result.Success = false;
                result.Message = "The user's id is null";
                return result;
            }

            //case 2: non-existent user

            var foundUser = await _accountServiceForWebApp.GetUserById(userId);

            if(foundUser == null)
            {
                result.Success = false;
                result.Message = $"The user '{userId}' does not exist";
                return result;
            }

            var entities = await _pedidoRepository.GetOrderListByUserId(userId);
            result.Result = _mapper.Map<List<PedidoDto>>(entities);
                
            return result;
        }
    }
}
