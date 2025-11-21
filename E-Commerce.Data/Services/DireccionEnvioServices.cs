using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.ViewModels;

namespace E_Commerce.Data.Services
{
    public class DireccionEnvioServices : BaseServices<DireccionEnvioViewModel, DireccionEnvioDto>, IDireccionEnvioServices
    {
        public readonly IDireccionEnvioRepository _direccionEnvioRepository;
        public readonly IMapper _mapper;
        public DireccionEnvioServices(IDireccionEnvioRepository direccionEnvioRepository, IMapper mapper) : base(mapper, direccionEnvioRepository)
        {
            _mapper = mapper;
            _direccionEnvioRepository = direccionEnvioRepository;
        }
    
    }
}
