using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Repository
{
    public interface ICuponRepository : IBaseRepository<Cupon>
    {
        Task<Cupon> GetCuponByCodeAsync(string code);
    }
}
