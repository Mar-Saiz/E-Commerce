using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class ListaDeseosRepository : BaseRepository<ListaDeseos>, IListaDeseosRepository
    {

        public readonly E_commenceContext _context;
        public readonly ProductoRepository _productoRepository;

        public ListaDeseosRepository(E_commenceContext context, ProductoRepository productoRepository) : base(context)
        {
            _context = context;
            _productoRepository = productoRepository;
        }

        
    }
}
