using AutoMapper;
using E_Commerce.Data.Core;
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
        public readonly ICuponServices _cuponServices;
        public CarritoItemServices(ICarritoItemRepository carrito, IMapper mapper) : base(mapper, carrito)
        {
            _mapper = mapper;
            _carritoItemServices = carrito;
        }

        //2024-0003 Maria Abreu

        public async Task<OperationResult<CarritoItemDto>> CreateCartAsync(CarritoItemDto AppUserId)
        {
            OperationResult<CarritoItemDto> result = new();

            var entitie = _mapper.Map<CarritoItem>(AppUserId);

            var Carrito = _mapper.Map<CarritoItemDto>(await _carritoItemServices.SaveEntityAsync(entitie));

            if (Carrito == null)
            {
                result.Success = false;
                result.Message = "Error al crear el carrito.";
                return result;
            }

            result.Result = Carrito;
            result.Success = true;

            return result;

        }
        public async Task<OperationResult<CarritoItemDto>> AddItemToCarritoAsync(int ProductID, int cantidad, CarritoItemDto Carrito)
        {
            OperationResult<CarritoItemDto> result = new();

            CarritoItem NewCarrito = new CarritoItem
            {
                ProductoId = ProductID,
                Cantidad = cantidad,
                UserId = Carrito.UserId
            };

            var CarritoConItem = _mapper.Map<CarritoItemDto>(await _carritoItemServices.UpdateEntityAsync(Carrito.Id, NewCarrito));

            if (CarritoConItem == null)
            {
                result.Success = false;
                result.Message = "Error al agregar el item al carrito.";
                return result;
            }

            result.Result = CarritoConItem;
            result.Success = true;

            return result;
        }
        public async Task<OperationResult<CarritoItemDto>> RemoveItemToCarritoAsync(int ProductID, int cantidad, CarritoItemDto Carrito)
        {
            OperationResult<CarritoItemDto> result = new();

            if (Carrito.ProductoId != ProductID)
            {
                result.Success = false;
                result.Message = "El producto no coincide con el producto del carrito.";
                return result;

            }

            CarritoItem NewCarrito = new CarritoItem
            {
                ProductoId = 0,
                Cantidad = Carrito.Cantidad > cantidad ? Carrito.Cantidad - cantidad : 0,
                UserId = Carrito.UserId
            };

            var CarritoSinItem = _mapper.Map<CarritoItemDto>(await _carritoItemServices.UpdateEntityAsync(Carrito.Id, NewCarrito));

            result.Result = CarritoSinItem;
            result.Success = true;

            return result;
        }
        public async Task<decimal> CalculateTotal(CarritoItemDto Carrito)
        {
            decimal total = 0;


            if (Carrito.Subtotal == 0)
            {
                var producto = await _productoServices.GetDtoById(Carrito.ProductoId);

                if (producto == null)
                {
                    return total;
                }

                return total = producto.Precio * Carrito.Cantidad * 0.18m;
            }

            decimal itbis = 0.18m;

            return total = Carrito.Subtotal * itbis;
        }
        public async Task<decimal> CalculateTotal(CarritoItemDto Carrito, CuponDto cuponDto)
        {
            decimal total = 0;

            var cupon = await _cuponServices.ValidarCuponAsync(cuponDto);

            if (Carrito.Subtotal == 0)
            {
                var producto = await _productoServices.GetDtoById(Carrito.ProductoId);

                if (producto == null)
                {
                    return total;
                }

                return total = producto.Precio * Carrito.Cantidad * 0.18m;
            }

            decimal itbis = 0.18m;

            if (cupon.Result == null)
            {
                Console.WriteLine("Cupón inválido o expirado.");
                return 0;
            }

            return total = (Carrito.Subtotal - cupon.Result.Descuento) * itbis;
        }

    }
}
