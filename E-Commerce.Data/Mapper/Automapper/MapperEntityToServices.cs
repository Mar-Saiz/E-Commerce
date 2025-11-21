using AutoMapper;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Base;

namespace E_Commerce.Data.Mapper.Automapper
{
    public class MapperEntityToServices : Profile
    {
        public MapperEntityToServices()
        {
            #region Base Mapper
            CreateMap(typeof(BaseEntityDto<>), typeof(BaseEntity<>)).ReverseMap();
            #endregion

            #region Mapper Entity to Dto
            CreateMap(typeof(CarritoItem), typeof(CarritoItemDto)).ReverseMap();
            CreateMap(typeof(Categoria), typeof(CategoriaDto)).ReverseMap();
            CreateMap(typeof(Cupon), typeof(CuponDto)).ReverseMap();
            CreateMap(typeof(DetallePedido), typeof(DetallePedidoDto)).ReverseMap();
            CreateMap(typeof(DireccionEnvio), typeof(DireccionEnvioDto)).ReverseMap();
            CreateMap(typeof(ListaDeseos), typeof(ListaDeseosDto)).ReverseMap();
            CreateMap(typeof(Pedido), typeof(PedidoDto)).ReverseMap();
            CreateMap(typeof(Producto), typeof(ProductoDto)).ReverseMap();
            CreateMap(typeof(Promocion), typeof(PromocionDto)).ReverseMap();
            CreateMap(typeof(Pagos), typeof(PagosDto)).ReverseMap(); //Addeded mapping for Pagos
            #endregion
        }
    }
}
