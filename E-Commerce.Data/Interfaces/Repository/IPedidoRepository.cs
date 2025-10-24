using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Repository
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        public List<Pedido> GetOrderListByUserId(string userId);
    }
}
