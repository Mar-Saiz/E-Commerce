using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class Cupon : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Codigo { get; set; }
        public decimal Descuento { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
