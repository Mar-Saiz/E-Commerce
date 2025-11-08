namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class CuponDto : BaseEntityDto<int>
    {
        public override int Id { get; set; }
        public string Codigo { get; set; }
        public decimal Descuento { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
