using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class ListaDeseos : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string UserId { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
