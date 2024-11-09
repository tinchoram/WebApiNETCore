using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPInteg.Api.Controllers;
using TPInteg.Persistance;
using TPInteg.Shared;
using FluentAssertions;

namespace TPInteg.Api.Tests
{
    public class ProductoControllerTests
    {
        private readonly ProductoController _controller;
        private readonly TPIntegradorDbContext _context;
        private readonly Mock<ILogger<ProductoController>> _loggerMock;

        public ProductoControllerTests()
        {
            var options = new DbContextOptionsBuilder<TPIntegradorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Producto_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TPIntegradorDbContext(options);
            _loggerMock = new Mock<ILogger<ProductoController>>();
            _controller = new ProductoController(_context, _loggerMock.Object);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!_context.Proveedor.Any())
            {
                var proveedorA = new Proveedor
                {
                    Nombre = "Proveedor A",
                    Direccion = "Direccion A",
                    Email = "proveedorA@example.com",
                    Telefono = "1234567890",
                    Localidad = new Localidad { Nombre = "Localidad A", CodigoPostal = 1000 }
                };
                _context.Proveedor.Add(proveedorA);

                _context.Producto.AddRange(new[]
                {
            new Producto { Codigo = "001", Descripcion = "Producto A", PrecioUnitario = 10.0, Stock = 100, Estado = "Activo", Proveedor = proveedorA },
            new Producto { Codigo = "002", Descripcion = "Producto B", PrecioUnitario = 15.0, Stock = 200, Estado = "Activo", Proveedor = proveedorA }
        });
                _context.SaveChanges();
            }
        }


        [Fact]
        public async Task Crear_ShouldReturnCreatedResult_WhenProductoIsValid()
        {
            var nuevoProducto = new Producto
            {
                Codigo = "003",
                Descripcion = "Producto C",
                PrecioUnitario = 20.0,
                Stock = 50,
                Estado = "Activo",
                ProveedorId = 1
            };
            var result = await _controller.Crear(nuevoProducto);
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task TraerPorId_ShouldReturnProducto_WhenIdExists()
        {
            var result = await _controller.TraerPorId(1);
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var producto = okResult?.Value as Producto;
            producto.Should().NotBeNull();
        }

        [Fact]
        public async Task Eliminar_ShouldReturnNoContent_WhenProductoIsDeleted()
        {
            var result = await _controller.Eliminar(1);
            result.Should().BeOfType<NoContentResult>();
            var producto = await _context.Producto.FindAsync(1);
            producto.Should().BeNull();
        }
    }
}
