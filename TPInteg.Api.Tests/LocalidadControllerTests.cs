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
    public class LocalidadControllerTests
    {
        private readonly LocalidadController _controller;
        private readonly TPIntegradorDbContext _context;
        private readonly Mock<ILogger<LocalidadController>> _loggerMock;

        public LocalidadControllerTests()
        {
            var options = new DbContextOptionsBuilder<TPIntegradorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Localidad_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TPIntegradorDbContext(options);
            _loggerMock = new Mock<ILogger<LocalidadController>>();
            _controller = new LocalidadController(_context, _loggerMock.Object);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!_context.Localidad.Any())
            {
                _context.Localidad.AddRange(new[]
                {
                    new Localidad { Id = 1, Nombre = "Localidad A", CodigoPostal = 1000 },
                    new Localidad { Id = 2, Nombre = "Localidad B", CodigoPostal = 2000 }
                });
                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task Crear_ShouldReturnCreatedResult_WhenLocalidadIsValid()
        {
            var nuevaLocalidad = new Localidad { Nombre = "Localidad C", CodigoPostal = 3000 };
            var result = await _controller.Crear(nuevaLocalidad);
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.StatusCode.Should().Be(201);
            var createdLocalidad = createdResult.Value as Localidad;
            createdLocalidad.Should().NotBeNull();
            createdLocalidad.Nombre.Should().Be("Localidad C");
        }

        [Fact]
        public async Task Crear_ShouldReturnBadRequest_WhenLocalidadIsInvalid()
        {
            _controller.ModelState.AddModelError("Nombre", "El nombre es requerido");
            var nuevaLocalidad = new Localidad { CodigoPostal = 3000 };
            var result = await _controller.Crear(nuevaLocalidad);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task TraerPorId_ShouldReturnLocalidad_WhenIdExists()
        {
            var result = await _controller.TraerPorId(1);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var localidad = okResult.Value as Localidad;
            localidad.Should().NotBeNull();
            localidad.Nombre.Should().Be("Localidad A");
        }

        [Fact]
        public async Task TraerPorId_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.TraerPorId(99);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task TraerPorNombre_ShouldReturnLocalidades_WhenNameContains()
        {
            var result = await _controller.TraerPorNombre("Localidad");
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var localidades = okResult.Value as List<Localidad>;
            localidades.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task TraerTodos_ShouldReturnAllLocalidades()
        {
            var result = await _controller.TraerTodos();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var localidades = okResult.Value as List<Localidad>;
            localidades.Should().HaveCount(2);
        }

        [Fact]
        public async Task Actualizar_ShouldReturnNoContent_WhenLocalidadIsUpdated()
        {
            var updatedLocalidad = new Localidad { Id = 1, Nombre = "Localidad Actualizada" };
            var result = await _controller.Actualizar(1, updatedLocalidad);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Eliminar_ShouldReturnNoContent_WhenLocalidadIsDeleted()
        {
            var result = await _controller.Eliminar(1);
            result.Should().BeOfType<NoContentResult>();
            var localidad = await _context.Localidad.FindAsync(1);
            localidad.Should().BeNull();
        }
    }
}
