using E_Commerce.Data.Entities;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class DetallePedidoDto : BaseEntityDto<int>
    {
        public override int Id { get; set; }

        public int PedidoId { get; set; }
        public PedidoDto Pedido { get; set; }

        public int ProductoId { get; set; }
        public ProductoDto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
