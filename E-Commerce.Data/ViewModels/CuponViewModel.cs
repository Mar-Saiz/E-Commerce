
namespace E_Commerce.Data.ViewModels
{
    public class CuponViewModel: IBaseViewModel<int>
    {
        public override int Id { get; set; }
        public string Codigo { get; set; }
        public decimal Descuento { get; set; }
        public DateTime? FechaExpiracion { get; set; }
    }
}
