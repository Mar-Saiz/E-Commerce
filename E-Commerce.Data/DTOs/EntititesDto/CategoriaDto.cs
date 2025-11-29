using E_Commerce.Data.Entities;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class CategoriaDto : CommonAtributtesDto
    {
        public override int Id { get; set; }

        public ICollection<ProductoDto>? Productos { get; set; }
    }
}
