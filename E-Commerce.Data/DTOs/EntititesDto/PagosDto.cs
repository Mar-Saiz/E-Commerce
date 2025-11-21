using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.DTOs.User;

namespace E_Commerce.Data.Entities
{
    public class PagosDto : BaseEntityDto<int>
    {
        public override int Id { get; set; }
        public  string UserId { get; set; }
        public UserDto? User { get; set; }
        public  string MetodoPago { get; set; }
        public  decimal Monto { get; set; }
        public  int PedidoId { get; set; }
        public ProductoDto? Pedido { get; set; }    


    }
}
