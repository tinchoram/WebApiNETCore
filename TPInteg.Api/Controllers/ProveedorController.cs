using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TPInteg.Api.Repositories;
using TPInteg.Shared;

namespace TPInteg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProveedorController : ControllerBase, IProveedorRepository
{
    private readonly ILogger<ProveedorController> _logger;
    public ProveedorController(ILogger<ProveedorController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Recibe un proveedor para actualizar
    /// </summary>
    /// <param name="data">Parametro Proveedor</param>
    /// <returns>OK si pudo actualizarlo</returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPatch]
    [ProducesResponseType(typeof(bool), 204)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Recibe un proveedor para actualizar")]
    [SwaggerOperation(Summary = "Actualizar un proveedor")]

    public async Task<IActionResult> Actualizar([FromBody] Proveedor data)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    [ProducesResponseType(typeof(int), 201)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 409)]
    [EndpointDescription("Recibe un proveedor para crear")]
    [SwaggerOperation(Summary = "Crear nuevo proveedor")]
    public async Task<IActionResult> Crear([FromBody] Proveedor data)
    {
        throw new NotImplementedException();
    }
    [HttpDelete]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Recibe un proveedor para eliminar")]
    [SwaggerOperation(Summary = "Eliminar un proveedor")]
    public async Task<IActionResult> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
    [HttpGet("TraerPorId")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Obtener datos de un proveedor por su ID")]
    [SwaggerOperation(Summary = "Traer un proveedor por ID")]
    public async Task<IActionResult> TraerPorId(int id)
    {
        throw new NotImplementedException();
    }
    [HttpGet("TraerPorNombre")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Obtener datos de proveedores que contengan un Nombre")]
    [SwaggerOperation(Summary = "Traer proveedores por Nombre")]
    public async Task<IActionResult> TraerPorNombre(string name)
    {
        throw new NotImplementedException();
    }
    [HttpGet("TraerTodos")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Obtener datos de todos los proveedores")]
    [SwaggerOperation(Summary = "Traer todos los proveedores")]
    public async Task<IActionResult> TraerTodos()
    {
        throw new NotImplementedException();
    }
}
