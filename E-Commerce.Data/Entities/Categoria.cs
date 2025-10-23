using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class Categoria : CommonAtributtes
    {
        public  override int Id { get; set; }
        
        public ICollection<Producto>? Productos { get; set; }
    }
}
