using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Services
{
    public interface IPedidoServices : IBaseServices<Pedido, PedidoDto>
    {
        public Task<List<PedidoDto>> GetOrderListByUserId(string userId);
    }
}
