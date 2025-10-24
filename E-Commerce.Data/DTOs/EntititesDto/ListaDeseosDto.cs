using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class ListaDeseosDto : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string? UserId { get; set; }

        public ICollection<ProductoDto>? Productos { get; set; }
    }
}
