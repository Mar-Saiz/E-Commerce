using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class ProductoServices : BaseServices<Producto, ProductoDto>, IProductoServices
    {
        public readonly IProductoRepository _productoRepository;
        public readonly IMapper _mapper;
        public ProductoServices(IProductoRepository productoRepository, IMapper mapper) : base(mapper, productoRepository)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;
        }
    
    }
}
