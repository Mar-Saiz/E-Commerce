using E_Commerce.Data.Context;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.ViewModels;

namespace E_Commerce.Data.Repositories
{
    public class DireccionEnvioRepository : BaseRepository<DireccionEnvioViewModel>, IDireccionEnvioRepository
    {

        public readonly E_commenceContext _context;

        public DireccionEnvioRepository(E_commenceContext context) : base(context)
        {
            _context = context;

        }
    
    }
}
