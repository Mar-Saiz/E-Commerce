using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class CarritoItem : BaseEntity<int>
    {

        public override int Id { get; set; }
        public string UserId { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal Subtotal => Producto != null ? Producto.Precio * Cantidad : 0;
    }
}
