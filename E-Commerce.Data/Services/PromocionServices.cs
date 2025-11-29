using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    internal class PromocionServices : BaseServices<Promocion, PromocionDto>, IPromocionServices
    {
        public readonly IPromocionRepository _promocionRepository;
        public readonly IMapper _mapper;
        public PromocionServices(IPromocionRepository promocionRepository, IMapper mapper) : base(mapper, promocionRepository)
        {
            _mapper = mapper;
            _promocionRepository = promocionRepository;
        }
    
    }
}
