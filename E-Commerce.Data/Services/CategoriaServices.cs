using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class CategoriaServices : BaseServices<Categoria, CategoriaDto> , ICategoriaServices
    {
        public readonly ICategoriaRepository _categoriaRepository;
        public readonly IMapper _mapper;
        public CategoriaServices(ICategoriaRepository categoriaRepository, IMapper mapper) : base(mapper, categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }
    }
}
