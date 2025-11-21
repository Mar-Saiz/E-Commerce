namespace E_Commerce.Data.Interfaces.Base
{
    public abstract class BaseEntity<Type>
    {
        public abstract Type Id { get; set; }

        public DateTime? CreatetAt { get; set; } = DateTime.UtcNow;

        public bool Activo { get; set; }

    }
}
