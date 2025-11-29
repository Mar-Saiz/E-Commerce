using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class Promocion : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
    }
}
