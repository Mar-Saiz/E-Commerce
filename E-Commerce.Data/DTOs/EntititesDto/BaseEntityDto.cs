namespace E_Commerce.Data.DTOs.EntititesDto
{
    public abstract class BaseEntityDto<Type>
    {
        public abstract Type Id { get; set; }

        public DateTime? CreatetAt { get; set; } = DateTime.UtcNow;

        public bool Activo { get; set; }
    }
}
