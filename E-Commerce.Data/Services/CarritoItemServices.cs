using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;

namespace E_Commerce.Data.Services
{
    public class CarritoItemServices : BaseServices<CarritoItem, CarritoItemDto> , ICarritoItemServices
    {
        public readonly ICarritoItemRepository _carritoItemServices;
        public readonly IMapper _mapper;
        public CarritoItemServices(ICarritoItemRepository carrito, IMapper mapper) : base(mapper, carrito)
        {
            _mapper = mapper;
            _carritoItemServices = carrito;
        }
    }
}
