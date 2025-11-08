using AutoMapper;
using E_Commerce.Data.Context;
using E_Commerce.Data.DTOs.EntititesDto;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Interfaces.Services;
using E_Commerce.Data.Repositories;
using E_Commerce.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    public class UnitTestCarritoItem
    {
        private readonly ICarritoItemRepository carrito;
        private readonly IMapper mapper;
        private readonly IProductoServices productoServices;
        private readonly ICuponServices cuponServices;
        private readonly DbContextOptions<E_commenceContext> _options;

        public UnitTestCarritoItem()
        {
            // Use a unique in-memory database name per test instance to avoid cross-test state.
            _options = new DbContextOptionsBuilder<E_commenceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            // Initialize mapper to avoid null reference
            var config = new MapperConfiguration(cfg => {

                cfg.AddProfile(
                    new E_Commerce.Data.Mapper.Automapper.MapperEntityToServices());

            });
            this.mapper = config.CreateMapper();

            var contextForCarrito = new E_commenceContext(_options);
            var contextForProducto = new E_commenceContext(_options);
            var contextForCupon = new E_commenceContext(_options);

            this.carrito = new CarritoItemRepository(contextForCarrito);

            this.productoServices = new ProductoServices(
                new E_Commerce.Data.Repositories.ProductoRepository(contextForProducto),
                this.mapper);

            this.cuponServices = new CuponServices(
                new E_Commerce.Data.Repositories.CuponRepository(contextForCupon),
                this.mapper);
        }

        [Fact]
        public void CreateCartAsync_ShouldCreateCart_WhenCartNotNull()
        {
            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);
            var newCart = new CarritoItemDto
            {
                UserId = "test-user-123"
            };
            // Act
            var result = carritoItemServices.CreateCartAsync(newCart).Result;
            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal("test-user-123", result.Result.UserId);
        }

        [Fact]
        public void CreateCartAsync_ShouldNotCreateCart_WhenCartNull()
        {
            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);
            CarritoItemDto newCart = null;
            // Act
            var result = carritoItemServices.CreateCartAsync(newCart).Result;
            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
        }

        [Fact]
        public void CreateCartAsync_ShouldNotCreateCart_WhenUserIdEmpty()
        {
            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);
            var newCart = new CarritoItemDto
            {
                UserId = "",

            };
            // Act
            var result = carritoItemServices.CreateCartAsync(newCart).Result;
            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
        }

        [Fact]
        public void CreateCartAsync_ShouldNotCreateCart_WhenCantidadNegative()
        {
            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);
            var newCart = new CarritoItemDto
            {
                UserId = "test-user-123",
                Cantidad = -5
            };
            // Act
            var result = carritoItemServices.CreateCartAsync(newCart).Result;
            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task AddItemToCart_ShouldAddItem_WhenDataValid()
        {
            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);

            var newCartItem = new CarritoItemDto
            {
                Id = 1,
                UserId = "test-user-123",
                ProductoId = 0,
                Cantidad = 0
            };

            await carritoItemServices.CreateCartAsync(newCartItem);

            int productId = 1;
            int cantidad = 2;

            // Act
            var result = carritoItemServices.AddItemToCarritoAsync(productId, cantidad, newCartItem).Result;
            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal("test-user-123", result.Result.UserId);
            Assert.Equal(1, result.Result.ProductoId);
            Assert.Equal(2, result.Result.Cantidad);

        }

        [Fact]
        public async Task RemoveItemToCarritoAsync_ShouldRemoveItem_WhenDataValid()
        {

            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);

            //crear producto
            await productoServices.SaveDtoAsync(new ProductoDto
            {
                Id = 1,
                Nombre = "Producto Test",
                Descripcion = "Descripcion Test",
                Precio = 100,
                Stock = 10
            });

            //crear carrito
            var newCartItem = await carritoItemServices.CreateCartAsync(new CarritoItemDto
            {
                Id = 1,
                UserId = "test-user-123",
                ProductoId = 1,
                Cantidad = 2
            });

            int productId = 1;
            int cantidad = 1;

            // Act
            var result = carritoItemServices.RemoveItemToCarritoAsync(productId, cantidad, newCartItem.Result).Result;
            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal("test-user-123", result.Result.UserId);
            Assert.Equal(0, result.Result.ProductoId);
            Assert.Equal(1, result.Result.Cantidad);
        } 

        //Calcular el total del carrito con Cupon

        [Fact]

        public async Task CalculateTotal_ShouldCalculateTotalWithCupon_WhenDataValid()
        {
            // Arrange
            var carritoItemServices = new CarritoItemServices(carrito, mapper, productoServices, cuponServices);
            //crear producto
            var productoDto = new ProductoDto
            {
                Id = 6,
                Nombre = "Producto Test",
                Descripcion = "Descripcion Test",
                Precio = 100,
                Stock = 10
            };

            //crear cupon
           var cuponDto = await cuponServices.SaveDtoAsync(new CuponDto
            {
                Id = 2,
                Codigo = "DESCUENTO10",
                Descuento = 10,
                FechaExpiracion = DateTime.UtcNow.AddDays(10)
            });

            //crear carrito
            var newCartItem = await carritoItemServices.CreateCartAsync(new CarritoItemDto
            {
                Id = 5,
                UserId = "test-user-123",
                ProductoId = 6,
                Cantidad = 2,
                Producto = productoDto

            });
   
            // Act
            var total = await carritoItemServices.CalculateTotal(newCartItem.Result, cuponDto);
            // Assert
            decimal expectedTotal = 212.4m;
            Assert.Equal(expectedTotal, total);






        }
    }
}
