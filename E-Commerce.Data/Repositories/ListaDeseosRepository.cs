using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories
{
    public class ListaDeseosRepository : BaseRepository<ListaDeseos>, IListaDeseosRepository
    {

        public readonly E_commenceContext _context;

        public ListaDeseosRepository(E_commenceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ListaDeseos>> GetWishListByUserId(string userId)
        {
            return await _context.ListasDeseos
                .Include(p => p.Productos)
                .Where(id => id.UserId == userId)
                .ToListAsync();
        }
    }
}
