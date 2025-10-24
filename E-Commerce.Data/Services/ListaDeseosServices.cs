using AutoMapper;
using E_Commerce.Data.Core;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class ListaDeseosServices : BaseServices<ListaDeseos, ListaDeseosDto>, IListaDeseosServices
    {
        public readonly IListaDeseosRepository _listaDeseosRepository;
        public readonly IAccountServiceForWebApp _accountServiceForWebApp;
        public readonly IMapper _mapper;

        public ListaDeseosServices(IListaDeseosRepository listaDeseosRepository, IMapper mapper, IAccountServiceForWebApp accountServiceForWebApp) : base(mapper, listaDeseosRepository)
        {
            _mapper = mapper;
            _listaDeseosRepository = listaDeseosRepository;
            _accountServiceForWebApp = accountServiceForWebApp;
        }

        public async Task<OperationResult<ListaDeseosDto>> GetWishListByUserId(string userId)
        {
            OperationResult<ListaDeseosDto> result = new();

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

            var entities = await _listaDeseosRepository.GetWishListByUserId(userId);
            result.Result = _mapper.Map<ListaDeseosDto>(entities);

            return result;
        }
    }
}
