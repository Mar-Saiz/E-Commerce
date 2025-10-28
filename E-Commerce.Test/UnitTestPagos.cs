using AutoMapper;
using E_Commerce.Data.Context;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    internal class UnitTestPagos
    {
        private readonly IPagosRepository pagoRepository;
        private readonly IMapper mapper;
        private readonly DbContextOptions<E_commenceContext> _options;

        public UnitTestPagos()
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
            var contextForPagos = new E_commenceContext(_options);

            //Configurar dependencias
            this.pagoRepository = new E_Commerce.Data.Repositories.PagosRepository(contextForPagos);

        }
    }
}
