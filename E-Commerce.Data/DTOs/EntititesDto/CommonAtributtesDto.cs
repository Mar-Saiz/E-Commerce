namespace E_Commerce.Data.DTOs.EntititesDto
{
    public abstract class CommonAtributtesDto: BaseEntityDto<int>
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
