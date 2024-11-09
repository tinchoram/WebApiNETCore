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
    public class ProveedorControllerTests
    {
        private readonly ProveedorController _controller;
        private readonly TPIntegradorDbContext _context;
        private readonly Mock<ILogger<ProveedorController>> _loggerMock;

        public ProveedorControllerTests()
        {
            var options = new DbContextOptionsBuilder<TPIntegradorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Proveedor_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TPIntegradorDbContext(options);
            _loggerMock = new Mock<ILogger<ProveedorController>>();
            _controller = new ProveedorController(_context, _loggerMock.Object);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!_context.Proveedor.Any())
            {
                var localidadA = new Localidad { Nombre = "Localidad A" };
                _context.Localidad.Add(localidadA);

                _context.Proveedor.AddRange(new[]
                {
                    new Proveedor { Nombre = "Proveedor A", Email = "proveedorA@example.com", Telefono = "1234567890", Direccion = "Direccion A", Localidad = localidadA },
                    new Proveedor { Nombre = "Proveedor B", Email = "proveedorB@example.com", Telefono = "0987654321", Direccion = "Direccion B", Localidad = localidadA }
                });
                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task Crear_ShouldReturnCreatedResult_WhenProveedorIsValid()
        {
            var nuevoProveedor = new Proveedor
            {
                Nombre = "Proveedor C",
                Email = "proveedorc@example.com",
                Telefono = "111222333",
                Direccion = "Direccion C",
                LocalidadId = 1
            };
            var result = await _controller.Crear(nuevoProveedor);
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult?.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task TraerPorId_ShouldReturnProveedor_WhenIdExists()
        {
            var result = await _controller.TraerPorId(1);
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var proveedor = okResult?.Value as Proveedor;
            proveedor.Should().NotBeNull();
            proveedor?.Nombre.Should().Be("Proveedor A");
        }

        [Fact]
        public async Task TraerPorNombre_ShouldReturnProveedores_WhenNameContains()
        {
            var result = await _controller.TraerPorNombre("Proveedor A");
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var proveedores = okResult.Value as List<Proveedor>;
            proveedores.Should().NotBeNull();
            proveedores!.Should().HaveCountGreaterOrEqualTo(1);
        }
    }
}
