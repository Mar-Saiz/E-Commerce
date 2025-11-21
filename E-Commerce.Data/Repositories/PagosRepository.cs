using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class PagosRepository : BaseRepository<Pagos>, IPagosRepository
    {
        public PagosRepository(E_commenceContext context) : base(context)
        {
        }
    }
}
