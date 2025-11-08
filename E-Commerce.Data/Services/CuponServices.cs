using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class CuponServices : BaseServices<Cupon, CuponDto>, ICuponServices
    {
        public readonly ICuponRepository _categoriaRepository;
        public readonly IMapper _mapper;
        public CuponServices(ICuponRepository cuponRepository, IMapper mapper) : base(mapper, cuponRepository)
        {
            _mapper = mapper;
            _categoriaRepository = cuponRepository;
        }
    
    }
}
