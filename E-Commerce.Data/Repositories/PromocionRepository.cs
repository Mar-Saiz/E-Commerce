using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class PromocionRepository : BaseRepository<Promocion>, IPromocionRepository
    {
        public readonly E_commenceContext _context;

        public PromocionRepository(E_commenceContext context) : base(context)
        {
            {
                _context = context;
            }
        }
    
    }
}
