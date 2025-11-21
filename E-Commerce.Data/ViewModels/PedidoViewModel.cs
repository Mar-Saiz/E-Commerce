using E_Commerce.Data.ViewModels;

namespace E_Commerce.Data.Entities
{
    public class PedidoViewModel : IBaseViewModel<int>
    {
        public override int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } // Ej: "Pendiente", "Enviado", "Entregado"

        public string UserId { get; set; }

        public ICollection<DetallePedidoViewModel> Detalles { get; set; }
        public DireccionEnvioViewModel DireccionEnvio { get; set; }
    }
}
