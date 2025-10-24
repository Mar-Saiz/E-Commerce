using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public readonly E_commenceContext _context;

        public PedidoRepository(E_commenceContext context) : base(context)
        {
            {
                _context = context;
            }
        }

        public List<Pedido> GetOrderListByUserId(string userId)
        {
            var orderList = _context.Pedidos
                .Include(dp => dp.Detalles)
                .Include(de => de.DireccionEnvio)
                .Where(id => id.UserId == userId)
                .ToList();

            return orderList;
        }
    }
}
