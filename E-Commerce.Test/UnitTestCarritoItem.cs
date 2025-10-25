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

                cfg.AddProfile(new E_Commerce.Data.Mapper.Automapper.MapperEntityToServices());

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

        public void CreateCartAsync_ShouldCreateCart_WhenCartIsNull()
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
            Assert.Equal("test-user-123", result.Result.UserId);
        }

    }
}
