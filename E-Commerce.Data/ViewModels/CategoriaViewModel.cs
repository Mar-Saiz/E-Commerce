namespace E_Commerce.Data.ViewModels
{
    public class CategoriaViewModel : IBaseViewModel<int>
    {
        public override int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public ICollection<ProductoViewModel>? Productos { get; set; }
    }
}
