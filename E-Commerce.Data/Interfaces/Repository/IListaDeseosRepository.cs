using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Repository
{
    public interface IListaDeseosRepository : IBaseRepository<ListaDeseos>
    {
        public Task<List<ListaDeseos>> GetWishListByUserId(string userId);
    }
}
