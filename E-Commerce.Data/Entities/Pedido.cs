using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class Pedido : BaseEntity<int>
    {
        public override int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } // Ej: "Pendiente", "Enviado", "Entregado"

        public string UserId { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
        public DireccionEnvio DireccionEnvio { get; set; }
    }
}
