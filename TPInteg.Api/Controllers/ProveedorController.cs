using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TPInteg.Persistance;
using TPInteg.Shared;

namespace TPInteg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProveedorController : ControllerBase
{
    private readonly TPIntegradorDbContext _context;
    private readonly ILogger<ProveedorController> _logger;

    public ProveedorController(TPIntegradorDbContext context, ILogger<ProveedorController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Proveedor), 201)]
    [ProducesResponseType(400)]
    [SwaggerOperation(Summary = "Crear nuevo proveedor")]
    public async Task<IActionResult> Crear([FromBody] Proveedor proveedor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _context.Proveedor.Add(proveedor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(TraerPorId), new { id = proveedor.Id }, proveedor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el proveedor.");
            return BadRequest($"Error creando el registro. Detalle: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Proveedor), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer un proveedor por ID")]
    public async Task<IActionResult> TraerPorId(int id)
    {
        var proveedor = await _context.Proveedor
            .Include(p => p.Localidad)
            .Include(p => p.Productos)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (proveedor == null)
        {
            return NotFound(new { message = $"El proveedor con ID {id} no fue encontrado." });
        }

        return Ok(proveedor);
    }

    [HttpGet("nombre/{name}")]
    [ProducesResponseType(typeof(List<Proveedor>), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer proveedores por Nombre")]
    public async Task<IActionResult> TraerPorNombre(string name)
    {
        var proveedores = await _context.Proveedor
            .Where(p => p.Nombre.Contains(name))
            .Include(p => p.Localidad)
            .ToListAsync();

        if (!proveedores.Any())
        {
            return NotFound(new { message = $"No se encontraron proveedores con el nombre '{name}'." });
        }

        return Ok(proveedores);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Proveedor>), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer todos los proveedores")]
    public async Task<IActionResult> TraerTodos()
    {
        var proveedores = await _context.Proveedor
            .Include(p => p.Localidad)
            .Include(p => p.Productos)
            .ToListAsync();

        if (!proveedores.Any())
        {
            return NotFound(new { message = "No se encontraron proveedores registrados." });
        }

        return Ok(proveedores);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [SwaggerOperation(Summary = "Actualizar un proveedor")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Proveedor data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var proveedor = await _context.Proveedor.FindAsync(id);
        if (proveedor == null)
        {
            return NotFound(new { message = $"El proveedor con ID {id} no fue encontrado." });
        }

        try
        {
            proveedor.Nombre = data.Nombre;
            proveedor.Apellido = data.Apellido;
            proveedor.NombreComercial = data.NombreComercial;
            proveedor.Direccion = data.Direccion;            
            proveedor.Email = data.Email;
            proveedor.Telefono = data.Telefono;
            proveedor.LocalidadId = data.LocalidadId;
            proveedor.Cuit = data.Cuit;
            proveedor.SitioWebUrl = data.SitioWebUrl;
            proveedor.Activo = data.Activo;
            proveedor.FechaNacimiento = data.FechaNacimiento;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al actualizar el proveedor con ID {id}.");
            return BadRequest($"Error actualizando el registro. Detalle: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Eliminar un proveedor")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var proveedor = await _context.Proveedor.FindAsync(id);
        if (proveedor == null)
        {
            return NotFound(new { message = $"El proveedor con ID {id} no fue encontrado." });
        }

        try
        {
            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al eliminar el proveedor con ID {id}.");
            return BadRequest($"Error eliminando el registro. Detalle: {ex.Message}");
        }
    }
}
