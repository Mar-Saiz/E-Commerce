using E_Commerce.Data.Context;
using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories
{
    public class ListaDeseosRepository : BaseRepository<ListaDeseos>, IListaDeseosRepository
    {
        public readonly E_commenceContext _context;
        public readonly IAccountServiceForWebApp _accountServiceForWebApp;

        public ListaDeseosRepository(E_commenceContext context, IAccountServiceForWebApp accountServiceForWebApp) : base(context)
        {
            _context = context;
            _accountServiceForWebApp = accountServiceForWebApp;
        }

        public async Task<OperationResult<ListaDeseos>> GetWishListByUserId(string userId)
        {
            OperationResult<ListaDeseos> result = new();

            //case 1: null or empty string

            if (string.IsNullOrWhiteSpace(userId))
            {
                result.Success = false;
                result.Message = "The user's id is null";
                return result;
            }

            //case 2: non-existent user

            var foundUser = await _accountServiceForWebApp.GetUserById(userId);

            if (foundUser == null)
            {
                result.Success = false;
                result.Message = $"The user '{userId}' does not exist";
                return result;
            }

            result.Result = await _context.ListasDeseos
                .Include(p => p.Productos)
                .Where(id => id.UserId == userId).FirstAsync();

            return result;
        }
    }
}
