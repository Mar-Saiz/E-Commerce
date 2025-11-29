namespace E_Commerce.Data.DTOs.EntititesDto
{
    public class PromocionDto : BaseEntityDto<int>
    {
        public override int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
    }
}
