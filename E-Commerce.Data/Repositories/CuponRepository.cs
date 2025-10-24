using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;

namespace E_Commerce.Data.Repositories
{
    public class CuponRepository : BaseRepository<Cupon>, ICuponRepository
    {

        public readonly E_commenceContext _context;

        public CuponRepository(E_commenceContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Cupon> GetCuponByCodeAsync(string code)
        {
            var result = _context.Cupones.FirstOrDefault(c => c.Codigo == code);

            if (result == null)
            {
                Console.WriteLine("Cupón no encontrado.");
                return null;
            }

            return result;
        }

    }
}
