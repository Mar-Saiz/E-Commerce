namespace E_Commerce.Data.Interfaces.Base
{
    public abstract class CommonAtributtes : BaseEntity<int>
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
