
namespace E_Commerce.Data.ViewModels
{
    public class PromocionViewModel: IBaseViewModel<int>
    {
        public override int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
    }
}
