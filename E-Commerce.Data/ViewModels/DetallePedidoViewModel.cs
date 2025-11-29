using E_Commerce.Data.Entities;

namespace E_Commerce.Data.ViewModels
{
    public class DetallePedidoViewModel : IBaseViewModel<int>
    {
        public override int Id { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
