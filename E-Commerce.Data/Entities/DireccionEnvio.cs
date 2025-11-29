using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class DireccionEnvio : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
