using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class ListaDeseosServices : BaseServices<ListaDeseos, ListaDeseosDto>, IListaDeseosServices
    {
        public readonly IListaDeseosRepository _listaDeseosRepository;
        public readonly IMapper _mapper;
        public ListaDeseosServices(IListaDeseosRepository listaDeseosRepository, IMapper mapper) : base(mapper, listaDeseosRepository)
        {
            _mapper = mapper;
            _listaDeseosRepository = listaDeseosRepository;
        }
    
    }
}
