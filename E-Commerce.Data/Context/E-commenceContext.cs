using E_Commerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Context
{
    public class E_commenceContext : DbContext
    {
        public E_commenceContext(DbContextOptions<E_commenceContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }
        public DbSet<DireccionEnvio> DireccionesEnvio { get; set; }
        public DbSet<ListaDeseos> ListasDeseos { get; set; }
        public DbSet<Cupon> Cupones { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Pagos> Pagos { get; set; }


    }



}
