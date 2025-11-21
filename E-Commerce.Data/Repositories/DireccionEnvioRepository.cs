using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class DireccionEnvioRepository : BaseRepository<DireccionEnvio>, IDireccionEnvioRepository
    {

        public readonly E_commenceContext _context;

        public DireccionEnvioRepository(E_commenceContext context) : base(context)
        {
            _context = context;

        }
    
    }
}
