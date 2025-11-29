 namespace E_Commerce.Data.ViewModels
{
    public class SaveProductosViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Marca { get; set; }
        public bool EsNuevo { get; set; }
        public bool EsPopular { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaViewModel? Categoria { get; set; }
    }
}
