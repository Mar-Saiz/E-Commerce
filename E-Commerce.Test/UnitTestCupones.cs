using AutoMapper;
using E_Commerce.Data.Context;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    public class UnitTestCupones
    {
        private readonly ICuponRepository cuponServices;
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
            this.cuponServices = new E_Commerce.Data.Repositories.CuponRepository(contextForCupon);
        }
    }
}
