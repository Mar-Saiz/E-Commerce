using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public readonly E_commenceContext _context;

        public ProductoRepository(E_commenceContext context) : base(context)
        {
            {
                _context = context;
            }
        }

    }
}
