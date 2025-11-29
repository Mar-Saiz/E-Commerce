using E_Commerce.Data.Entities;

namespace E_Commerce.Data.ViewModels
{
    public class DireccionEnvioViewModel : IBaseViewModel<int>
    {
        public override int Id { get; set; }
        public required string Calle { get; set; }
        public required string Ciudad { get; set; }
        public required string CodigoPostal { get; set; }
        public required string Pais { get; set; }

        public int? PedidoId { get; set; }
        public PedidoViewModel? Pedido { get; set; }
    }
}
