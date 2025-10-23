using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class PedidoServices : BaseServices<Pedido, PedidoDto>, IPedidoServices
    {
        public readonly IPedidoRepository _pedidoRepository;
        public readonly IMapper _mapper;
        public PedidoServices(IPedidoRepository pedidoRepository, IMapper mapper) : base(mapper, pedidoRepository)
        {
            _mapper = mapper;
           _pedidoRepository = pedidoRepository;
        }
    
    }
}
