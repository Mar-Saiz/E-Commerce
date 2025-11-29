using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Entities
{
    public class Producto : CommonAtributtes
    {
        public override int Id { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Marca { get; set; }
        public bool EsNuevo { get; set; }
        public bool EsPopular { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
