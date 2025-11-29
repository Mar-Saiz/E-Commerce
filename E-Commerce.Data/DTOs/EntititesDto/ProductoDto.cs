using E_Commerce.Data.Entities;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class ProductoDto : CommonAtributtesDto
    {
        public override int Id { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Marca { get; set; }
        public bool EsNuevo { get; set; }
        public bool EsPopular { get; set; }

        public int CategoriaId { get; set; }
        public CategoriaDto? Categoria { get; set; }
    }
}
