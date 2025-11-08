using AutoMapper;
using E_Commerce.Data.Context;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    public class UnitTestCupones
    {
        private readonly ICuponRepository cuponRepository;
        private readonly IMapper mapper;
        private readonly DbContextOptions<E_commenceContext> _options;

        public UnitTestCupones()
        {
            // configurar la BD en memoria
            _options = new DbContextOptionsBuilder<E_commenceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


            //Configurar autoMapper
            var config = new MapperConfiguration(cfg => {

                cfg.AddProfile(new E_Commerce.Data.Mapper.Automapper.MapperEntityToServices());

            });
            this.mapper = config.CreateMapper();

            //Configurar contextos
            var contextForCupon = new E_commenceContext(_options);

            //Configurar dependencias
            this.cuponRepository = new E_Commerce.Data.Repositories.CuponRepository(contextForCupon);
        }


        [Fact]
        public async void ValidarCuponAsync_ShoulValidateCoupn_WhenCouponValid()
        {
            // Arrange
            var cuponServices = new E_Commerce.Data.Services.CuponServices(cuponRepository, mapper);
            //Crear cupon válido
            var validCupon = new E_Commerce.Data.DTOs.EntititesDto.CuponDto
            {
                Codigo = "DESCUENTO10",
                Descuento = 10,
                FechaExpiracion = DateTime.Now.AddDays(10),
                Activo = true,
                Id = 1

            };

            await cuponServices.SaveDtoAsync(validCupon);

            // Act

            var result = cuponServices.ValidarCuponAsync(validCupon).Result;

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async void GetCuponByCodeAsync_ShouldFindCoupon_WhenCouponExists()
        {
            // Arrange
            var cuponServices = new E_Commerce.Data.Services.CuponServices(cuponRepository, mapper);

            var validCupon = new E_Commerce.Data.DTOs.EntititesDto.CuponDto
            {
                Codigo = "DESCUENTO10",
                Descuento = 10,
                FechaExpiracion = DateTime.Now.AddDays(10)
            };

            await cuponServices.SaveDtoAsync(validCupon);

            string code = validCupon.Codigo;

            // Act

            var resultSave = cuponServices.GetCuponByCodeAsync(code).Result;

            // Assert
            Assert.True(resultSave.Success);
        }

    }
}
