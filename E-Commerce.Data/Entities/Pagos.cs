using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class Pagos : BaseEntityDto<int>
    {
        public override int Id { get; set; }
        public  string UserId { get; set; }
        public  string MetodoPago { get; set; }
        public  decimal Monto { get; set; }
        public  int PedidoId { get; set; }


    }
}
