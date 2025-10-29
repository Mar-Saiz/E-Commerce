using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

        public CarritoItemServices(
            ICarritoItemRepository carrito, 
            IMapper mapper, 
            IProductoServices productoServices, 
            ICuponServices cuponServices) 
            : base(mapper, carrito)
        {
            _mapper = mapper;
            _carritoItemServices = carrito;
            _productoServices = productoServices;
            _cuponServices = cuponServices;
        }

        //2024-0003 Maria Abreu
        private bool ValidateCarritoItem(CarritoItemDto carritoItemDto)
        {

            if (carritoItemDto == null)
            {
                return false;
            }
            else if (carritoItemDto.Cantidad < 0)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(carritoItemDto.UserId))
            {
                return false;
            }

            return true;
        }
        public async Task<OperationResult<CarritoItemDto>> CreateCartAsync(
            CarritoItemDto CarritoDto)
        {
            OperationResult<CarritoItemDto> result = new();

            if(ValidateCarritoItem(CarritoDto) == false)
            {
                result.Success = false;
                result.Message = "Datos del carrito inválidos.";
                return result;
            }

            var entitie = _mapper.Map<CarritoItem>(CarritoDto);

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
        public async Task<OperationResult<CarritoItemDto>> AddItemToCarritoAsync(
            int ProductID, int cantidad, CarritoItemDto CarritoDto)
        {
            OperationResult<CarritoItemDto> result = new();

            if (ValidateCarritoItem(CarritoDto) == false)
            {
                result.Success = false;
                result.Message = "Datos del carrito inválidos.";
                return result;
            }

            if(ProductID < 0 || cantidad < 0)
            {
                result.Success = false;
                result.Message = "ID de producto o cantidad inválidos.";
                return result;
            }


            CarritoItem NewCarrito = new CarritoItem
            {
                ProductoId = ProductID,
                Cantidad = cantidad,
                UserId = CarritoDto.UserId,
                Id = CarritoDto.Id

            };

            var CarritoConItem = _mapper.Map<CarritoItemDto>(await _carritoItemServices.UpdateEntityAsync(CarritoDto.Id, NewCarrito));

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
        public async Task<OperationResult<CarritoItemDto>> RemoveItemToCarritoAsync(
            int ProductID, int cantidad, CarritoItemDto Carrito)
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
                UserId = Carrito.UserId,
                Id = Carrito.Id
            };

            var CarritoSinItem = _mapper.Map<CarritoItemDto>(await _carritoItemServices.UpdateEntityAsync(Carrito.Id, NewCarrito));

            result.Result = CarritoSinItem;
            result.Success = true;

            return result;
        }
        public async Task<decimal> CalculateTotal(
            CarritoItemDto Carrito)
        {
            decimal total = 0;

            if (ValidateCarritoItem(Carrito) == false)
            {
                return 0;
            }


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
        public async Task<decimal> CalculateTotal(
            CarritoItemDto Carrito, CuponDto cuponDto)
        {
           await _cuponServices.ValidarCuponAsync(cuponDto);

            decimal total = 0;

            if (ValidateCarritoItem(Carrito) == false)
            {
                return 0;
            }

            var cupon = await _cuponServices.ValidarCuponAsync(cuponDto);

            if (cupon.Result == null)
            {
                return 0;
            }

           return await ApplyCupon(cupon.Result, Carrito, 18);

        }

        private async Task<decimal> ApplyCupon(CuponDto cuponDto, CarritoItemDto CarritoDto, int itbis)
        {
            var CuponIsValid = _cuponServices.ValidarCuponAsync(cuponDto);

            var CartIsValid = ValidateCarritoItem(CarritoDto);

            if(CuponIsValid.Result.Success == false)
            {
               Console.WriteLine("Cupón inválido.");
                return 0;
            }
            else if(CartIsValid == false)
            {
                Console.WriteLine("Carrito inválido.");
                return 0;
            }

         var productoDto = await _productoServices.GetDtoById(CarritoDto.ProductoId);

            if (productoDto == null)
            {
                Console.WriteLine("Producto inválido.");
                return 0;
            }

            var precioFinal = (CarritoDto.Subtotal + ((CarritoDto.Subtotal) * itbis) / 100);

            var total=  precioFinal - (precioFinal * ( cuponDto.Descuento / 100));

            return total;
        }

    }
}
