using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class CarritoItemRepository : BaseRepository<CarritoItem>, ICarritoItemRepository
    {

        public readonly E_commenceContext _context;

        public CarritoItemRepository(E_commenceContext context) : base(context)
        {
            {
               _context = context;

            }
        }
    }
}
