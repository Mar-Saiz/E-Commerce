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
        public readonly IProductoServices _productoServices;
        public CarritoItemServices(ICarritoItemRepository carrito, IMapper mapper) : base(mapper, carrito)
        {
            _mapper = mapper;
            _carritoItemServices = carrito;
        }

        public async Task<CarritoItemDto> CreateCartAsync (CarritoItemDto AppUserId)
        {
            var entitie = _mapper.Map<CarritoItem>(AppUserId);

           return  _mapper.Map<CarritoItemDto>( await _carritoItemServices.SaveEntityAsync(entitie));

        }

        public async Task<CarritoItemDto> AddItemToCarritoAsync(int ProductID,int cantidad, CarritoItemDto Carrito)
        {
         
            CarritoItem NewCarrito = new CarritoItem
            {
                ProductoId = ProductID,
                Cantidad = cantidad,
                UserId = Carrito.UserId
            };

            var result = _mapper.Map<CarritoItemDto>( await _carritoItemServices.UpdateEntityAsync(Carrito.Id, NewCarrito));

            return result;
        }

        public async Task<CarritoItemDto> RemoveItemToCarritoAsync(int ProductID, int cantidad, CarritoItemDto Carrito)
        {

            if (Carrito.ProductoId != ProductID)
            {
                Console.WriteLine("El producto no coincide con el producto del carrito.");
                return null;
            }

            CarritoItem NewCarrito = new CarritoItem
            {
               ProductoId = 0,
               Cantidad = Carrito.Cantidad > cantidad ? Carrito.Cantidad - cantidad : 0,
               UserId = Carrito.UserId
            };

             var result = _mapper.Map<CarritoItemDto>(await _carritoItemServices.UpdateEntityAsync(Carrito.Id, NewCarrito));

            return result;
        }

        public async Task<decimal> CalculateTotal(CarritoItemDto Carrito)
        { decimal total = 0;

            if (Carrito.Subtotal == 0)
            {
               var producto = await _productoServices.GetDtoById(Carrito.ProductoId);

              return  total = producto.Precio * Carrito.Cantidad * 0.18m;
            }

            decimal itbis = 0.18m;

            return total = Carrito.Subtotal * itbis;

        }

    }
}
