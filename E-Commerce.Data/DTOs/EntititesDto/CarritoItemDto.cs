using E_Commerce.Data.Entities;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class CarritoItemDto : BaseEntityDto<int>
    {
        public override int Id { get; set; }
        public string UserId { get; set; }

        public int ProductoId { get; set; }
        public ProductoDto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal Subtotal => Producto != null ? Producto.Precio * Cantidad : 0;
    }

}
