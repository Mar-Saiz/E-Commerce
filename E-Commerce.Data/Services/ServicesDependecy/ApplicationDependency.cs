

using E_Commerce.Data.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Data.Services.ServicesDependecy
{
    public static class ApplicationDependency
    {
        public static void AddApplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(ApplicationDependency).Assembly);

            #region Services IOC
            service.AddTransient<ICarritoItemServices, CarritoItemServices>();
            service.AddTransient<ICategoriaServices, CategoriaServices>();
            service.AddTransient<ICuponServices, CuponServices>();
            service.AddTransient<IDetallePedidoServices, DetallePedidoServices>();
            service.AddTransient<IDireccionEnvioServices, DireccionEnvioServices>();
            service.AddTransient<IListaDeseosServices, ListaDeseosServices>();
            service.AddTransient<IPedidoServices, PedidoServices>();
            service.AddTransient<IProductoServices, ProductoServices>();
            service.AddTransient<IPromocionServices, PromocionServices>();
            service.AddTransient<IPagosServices, PagosServices>();
            #endregion

        }
    }
}
