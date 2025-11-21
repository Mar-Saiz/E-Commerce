using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class PedidoDto : BaseEntity<int>
    {
        public override int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } // Ej: "Pendiente", "Enviado", "Entregado"

        public string UserId { get; set; }

        public ICollection<DetallePedidoDto> Detalles { get; set; }
        public DireccionEnvioDto DireccionEnvio { get; set; }
    }
}
