using E_Commerce.Data.Core;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Services;

namespace E_Commerce.Data.Interfaces.Services
{
    public interface ICarritoItemServices : IBaseServices<CarritoItem, CarritoItemDto>
    {
        Task<OperationResult<CarritoItemDto>> CreateCartAsync(CarritoItemDto AppUserId);
        Task<OperationResult<CarritoItemDto>> AddItemToCarritoAsync(int ProductID, int cantidad, CarritoItemDto Carrito);
        Task<OperationResult<CarritoItemDto>> RemoveItemToCarritoAsync(int ProductID, int cantidad, CarritoItemDto Carrito);
        Task<decimal> CalculateTotal(CarritoItemDto Carrito);
        Task<decimal> CalculateTotal(CarritoItemDto Carrito, CuponDto cuponDto);

    }
}
