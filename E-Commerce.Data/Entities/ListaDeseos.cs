using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class ListaDeseos : BaseEntity<int>
    {
        public override int Id { get; set; }
        public required string UserId { get; set; }

        public required ICollection<Producto> Productos { get; set; }
    }
}
