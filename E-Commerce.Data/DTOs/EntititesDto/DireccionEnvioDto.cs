using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class DireccionEnvioDto : BaseEntityDto<int>
    {
        public override int Id { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }

        public int PedidoId { get; set; }
        public PedidoDto Pedido { get; set; }

    }
}
