using Moq;
using E_Commerce.Data.Context;
using E_Commerce.Data.Interfaces;
using E_Commerce.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    public class UnitTestListaDeseosRepository
    {
        private readonly Mock<IAccountServiceForWebApp> _mockAccountService;
        private readonly E_commenceContext _context;
        private readonly ListaDeseosRepository _listaDeseosRepository;
        
        public UnitTestListaDeseosRepository()
        {
            var options = new DbContextOptionsBuilder<E_commenceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new E_commenceContext(options);

            _mockAccountService = new Mock<IAccountServiceForWebApp>();

            _listaDeseosRepository = new ListaDeseosRepository(_context, _mockAccountService.Object);
        }

        [Fact]
        public async Task GetWishListByUserId_Should_Return_False_When_UserId_IsNull()
        {
            #region Arrange
            string? userId = null;
            string expectedMessage = "The user's id is null";
            #endregion

            #region Act
            var result = await _listaDeseosRepository.GetWishListByUserId(userId);
            #endregion

            #region Assert
            Assert.False(result.Success);
            Assert.Equal(expectedMessage, result.Message);
            #endregion
        }
    }
}