using AutoMapper;
using E_Commerce.Data.Context;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    public class UnitTestPagos
    {
        private readonly IPagosRepository pagosRepository;
        private readonly IMapper mapper;
        private readonly DbContextOptions<E_commenceContext> _options;

        public UnitTestPagos()
        {
            // configurar la BD en memoria
            _options = new DbContextOptionsBuilder<E_commenceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            //Configurar autoMapper

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new E_Commerce.Data.Mapper.Automapper.MapperEntityToServices());
            });

            this.mapper = config.CreateMapper();

            //Configurar contextos  
            var contextForPagos = new E_commenceContext(_options);

            //Configurar dependencias
            this.pagosRepository = new E_Commerce.Data.Repositories.PagosRepository(contextForPagos);

        }

        [Fact]
        public void ValidatePaymentDetails_ShouldReturnFalse_WhenPaymentDetailsInvalid()
        {
            // Arrange
            var pagosServices = new PagosServices(mapper, null!, pagosRepository);
            var invalidPagosDto = new PagosDto
            {
                UserId = "",
                MetodoPago = "Tarjeta",
                Monto = -50,
                PedidoId = 0
            };

            // Act
            var result = pagosServices.ValidatePaymentDetails(invalidPagosDto);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePaymentDetails_ShouldReturnTrue_WhenPaymentDetailsValid()
        {
            // Arrange
            var pagosServices = new PagosServices(mapper, null!, pagosRepository);
            var validPagosDto = new PagosDto
            {
                UserId = "user-123",
                MetodoPago = "PayPal",
                Monto = 100,
                PedidoId = 1
            };
            // Act
            var result = pagosServices.ValidatePaymentDetails(validPagosDto);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void SelectPaymentMethod_ShouldReturnSuccess_WhenMethodValid()
        {
            // Arrange
            var pagosServices = new PagosServices(mapper, null!, pagosRepository);
            var pagosDto = new PagosDto
            {
                UserId = "user-123",
                Monto = 100,
                PedidoId = 1
            };
            int metodoSeleccionado = 1; // PayPal
            // Act
            var result = await pagosServices.SelectPaymentMethod(metodoSeleccionado, pagosDto);
            // Assert
            Assert.True(result.Success);
            Assert.Equal("PayPal", result.Result.MetodoPago);

        }

        [Fact]
        public async void SelectPaymentMethod_ShouldReturnFailure_WhenMethodInvalid()
        {
            // Arrange
            var pagosServices = new PagosServices(mapper, null!, pagosRepository);
            var pagosDto = new PagosDto
            {
                UserId = "user-123",
                Monto = 100,
                PedidoId = 1
            };
            int metodoSeleccionado = 5; // Método inválido
            // Act
            var result = await pagosServices.SelectPaymentMethod(metodoSeleccionado, pagosDto);
            // Assert
            Assert.False(result.Success);
            Assert.Equal("Método de pago inválido.", result.Message);
        }

        [Fact]

        public async void ConfirmPurchase_ShouldReturnSucces_WhenPagosValid()
        {
            //Arrange
            var pagosServices = new PagosServices(mapper, null!, pagosRepository);
            var pagosDto = new PagosDto
            {
                UserId = "user-123",
                MetodoPago = "Tarjeta",
                Monto = 150,
                PedidoId = 2
            };

            //Act
            var result = await pagosServices.ConfirmPurchase(pagosDto);

            //Assert
            Assert.True(result.Success);
        }

    }
}
