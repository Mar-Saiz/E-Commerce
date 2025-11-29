namespace E_Commerce.Data.Entities
{
    public class FiltroDeProductos
    {
        public int? CategoriaId { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public string? Marca { get; set; }
        public List<string>? Marcas { get; set; }
        public bool? EsNuevo { get; set; }
        public bool? EsPopular { get; set; }
        public int? StockMinimo { get; set; }
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; } = "Id";
        public bool SortDescending { get; set; } = false;
    }
}
