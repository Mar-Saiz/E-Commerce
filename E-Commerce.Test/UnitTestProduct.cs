using E_Commerce.Data.Context;
using E_Commerce.Data.Core;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces.Repository;
using E_Commerce.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Test
{
    public class UnitTestProduct
    {
        private readonly IProductoRepository _repository;
        private readonly E_commenceContext _context;
        public UnitTestProduct()
        {
            _repository = new ProductoRepository(new Data.Context.E_commenceContext());

            var options = new DbContextOptionsBuilder<E_commenceContext>()
              .UseInMemoryDatabase(databaseName: "ECommerce_TestDB")
              .Options;

            _context = new E_commenceContext(options);
        }

        [Fact]
        public void GetByCategoriesAsync_ShouldReturnFailure_WhenCategoryIsNull()
        {
            // Arrange
            Producto producto = null;
            var result = _repository.AddProduct(producto);
            

            // Act
            string message = "El objeto producto es requerido.";

            // Assert
            Assert.IsType<OperationResult<Producto>>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);


        }

        [Fact]
        public void AddProduct_ShouldReturnFailure_WhenProductIsNull()
        {
            // Arrange
            Producto producto = null;

            // Act
            var result = _repository.AddProduct(producto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El objeto producto es requerido.", result.Message);
        }

        [Fact]
        public void AddProduct_ShouldReturnFailure_WhenNameIsEmpty()
        {
            // Arrange
            var producto = new Producto { Nombre = "", Precio = 100 };

            // Act
            var result = _repository.AddProduct(producto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El nombre del producto es requerido", result.Message);
        }

        [Fact]
        public void AddProduct_ShouldReturnFailure_WhenNameAlreadyExists()
        {
            // Arrange
            var producto1 = new Producto { Nombre = "Laptop", Precio = 1200 };
            var producto2 = new Producto { Nombre = "Laptop", Precio = 1500 };

            _repository.AddProduct(producto1);

            // Act
            var result = _repository.AddProduct(producto2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El nombre del producto ya se encuentra registrado.", result.Message);
        }

        [Fact]
        public void AddProduct_ShouldReturnSuccess_WhenValidProduct()
        {
            // Arrange
            var producto = new Producto { Nombre = "Mouse", Precio = 50 };

            // Act
            var result = _repository.AddProduct(producto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Producto Agregado con exito", result.Message);
        }

        [Fact]
        public async Task GetByCategoriesAsync_ShouldReturnFailure_WhenCategoryIsNegative()
        {
            // Act
            var result = await _repository.GetByCategoriesAsync(-1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El numero de categoria no puede ser menor a 0", result.Message);
        }

        [Fact]
        public async Task GetByCategoriesAsync_ShouldReturnProducts_WhenCategoryExists()
        {
            // Arrange
            var categoriaId = 1;
            
            var producto1 = new Producto { Nombre = "Teclado", CategoriaId = categoriaId, Stock = 5 };
            var producto2 = new Producto { Nombre = "Pantalla", CategoriaId = categoriaId, Stock = 2 };

            _repository.AddProduct(producto1);
            _repository.AddProduct(producto2);
            await _context.SaveChangesAsync();
            

            // Act
            var result = await _repository.GetByCategoriesAsync(categoriaId);
            string message = "El numero de categoria ya se encuentra registrado.";

            // Assert
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetByPrecioRangeAsync_ShouldReturnFailure_WhenMinGreaterThanMax()
        {
            // Act
            var result = await _repository.GetByPrecioRangeAsync(500, 100);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El precio mínimo no puede ser mayor al precio máximo", result.Message);
        }

        [Fact]
        public async Task GetByPrecioRangeAsync_ShouldReturnSuccess_WhenValidRange()
        {
            // Arrange

            var producto1 = new Producto { Nombre = "Teclado", CategoriaId = 1, Stock = 5 , Precio =100};
            var producto2 = new Producto { Nombre = "Pantalla", CategoriaId = 2, Stock = 2 , Precio = 400};
            _repository.AddProduct(producto1);
            _repository.AddProduct(producto2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByPrecioRangeAsync(100, 400);
            var message = $"Se encontraron productos en el rango de precio";
            // Assert
            Assert.True(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetNewAsync_ShouldReturnFailure_WhenCantidadIsZero()
        {
            // Act
            var result = await _repository.GetNewAsync(0);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La cantidad debe ser mayor a cero", result.Message);
        }

        [Fact]
        public async Task GetPopularAsync_ShouldReturnFailure_WhenCantidadIsZero()
        {
            // Act
            var result = await _repository.GetPopularAsync(0);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La cantidad debe ser mayor a cero", result.Message);
        }

        [Fact]
        public async Task GetPopularAsync_ShouldReturnSuccess_WhenPopularProductsExist()
        {
            // Arrange
            _context.Productos.Add(new Producto { Nombre = "iPhone", EsPopular = true, Stock = 10 });
            _context.Productos.Add(new Producto { Nombre = "Galaxy", EsPopular = true, Stock = 8 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetPopularAsync(3);

            // Assert
            Assert.True(result.Success);
            Assert.All(result.Result, p => Assert.True(p.EsPopular));
        }

        [Fact]
        public async Task GetProductByBrand_ShouldReturnFailure_WhenBrandIsEmpty()
        {
            // Act
            var result = await _repository.GetProductByBrand("");
            var message = "La marca no puede estar vacía";

            // Assert
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetProductByBrand_ShouldReturnSuccess_WhenBrandExists()
        {
            // Arrange
            var producto1 = new Producto { Nombre = "Teclado", CategoriaId = 1, Stock = 5, Precio = 100 , Marca = "Logitech" };
            _repository.AddProduct(producto1);
            var producto2 = new Producto { Nombre = "Teclado", CategoriaId = 1, Stock = 5, Precio = 100, Marca = "Logitech" };
            _repository.AddProduct(producto2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductByBrand("Logitech");
            string message = $"La marca del producto ya esta registrado.";

            // Assert
            Assert.False(result.Success);  
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateProduct_ShouldReturnFailure_WhenProductIsNull()
        {
            // Arrange
            Producto producto = null;

            // Act
            var result = _repository.UpdateProduct(producto);

            // Assert
            string message = "El objeto producto es requerido.";
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateProduct_ShouldReturnFailure_WhenNameIsEmpty()
        {
            // Arrange
            var producto = new Producto { Nombre = "" };

            // Act
            var result = _repository.UpdateProduct(producto);

            // Assert
            string message = "El nombre del producto es requerido";
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateProduct_ShouldReturnFailure_WhenNameLenghIsLargeMax()
        {
            // Arrange
            var producto = new Producto { Nombre = "hfkjdfhdkjfhskjfhskdfjhsdk3jfhsdkjfhsdkjfhskfeufwifweghwuieghweb" };

            // Act
            var result = _repository.AddProduct(producto);

            // Assert
            string message = "El nombre del producto sobrepasa el limite definido de 60";
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void AddProduct_ShouldReturnFailure_WhenNameLenghIsLargeMax()
        {
            // Arrange
            var producto = new Producto { Nombre = "hfkjdfhdkjfhskjfhskdfjhsdk3jfhsdkjfhsdkjfhskfeufwifweghwuieghweb" };

            // Act
            var result = _repository.AddProduct(producto);

            // Assert
            string message = "El nombre del producto sobrepasa el limite definido de 60";
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateProduct_ShouldReturnFailure_WhenNameAlreadyExists()
        {
            // Arrange
            var productoExistente = new Producto { Nombre = "Laptop", Precio = 1000 };
            _repository.AddProduct(productoExistente);

            var productoDuplicado = new Producto { Nombre = "Laptop", Precio = 1200 };

            // Act
            var result = _repository.UpdateProduct(productoDuplicado);

            // Assert
            string message = "El nombre del producto ya se encuentra registrado.";
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateProduct_ShouldReturnSuccess_WhenValidProduct()
        {
            // Arrange
            var productoOriginal = new Producto { Nombre = "Mouse", Precio = 50 };
            _repository.AddProduct(productoOriginal);

            // Simula un cambio de nombre y precio
            productoOriginal.Nombre = "Mouse Gamer";
            productoOriginal.Precio = 75;

            // Act
            var result = _repository.UpdateProduct(productoOriginal);

            // Assert
            string message = "Producto actualizado con exito";
            Assert.True(result.Success);
            Assert.Equal(message, result.Message);
        }
    }
}
