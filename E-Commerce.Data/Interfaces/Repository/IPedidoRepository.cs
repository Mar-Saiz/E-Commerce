using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Repository
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        public Task<OperationResult<List<Pedido>>> GetOrderListByUserId(string userId);
    }
}
