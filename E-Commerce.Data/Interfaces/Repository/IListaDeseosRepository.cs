using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;

namespace E_Commerce.Data.Interfaces.Repository
{
    public interface IListaDeseosRepository : IBaseRepository<ListaDeseos>
    {
        public Task<OperationResult<ListaDeseos>> GetWishListByUserId(string userId);
    }
}
