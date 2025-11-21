using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class DetallePedidoServices : BaseServices<DetallePedido, DetallePedidoDto>, IDetallePedidoServices
    {
        public readonly IDetallePedidoRepository _detallePedidoRepository;
        public readonly IMapper _mapper;
        public DetallePedidoServices(IDetallePedidoRepository detallePedidoRepository, IMapper mapper) : base(mapper, detallePedidoRepository)
        {
            _mapper = mapper;
            _detallePedidoRepository = detallePedidoRepository;
        }
    
    }
}
