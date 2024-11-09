using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TPInteg.Persistance;
using TPInteg.Shared;

namespace TPInteg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LocalidadController : ControllerBase
{
    private readonly TPIntegradorDbContext _context;
    private readonly ILogger<LocalidadController> _logger;

    public LocalidadController(TPIntegradorDbContext context, ILogger<LocalidadController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Localidad), 201)]
    [ProducesResponseType(400)]
    [SwaggerOperation(Summary = "Crear nueva localidad")]
    public async Task<IActionResult> Crear([FromBody] Localidad localidad)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _context.Localidad.Add(localidad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(TraerPorId), new { id = localidad.Id }, localidad);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear la localidad.");
            return BadRequest($"Error creando el registro. Detalle: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Localidad), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer una localidad por ID")]
    public async Task<IActionResult> TraerPorId(int id)
    {
        var localidad = await _context.Localidad.FindAsync(id);
        return localidad == null ? NotFound() : Ok(localidad);
    }

    [HttpGet("nombre/{name}")]
    [ProducesResponseType(typeof(List<Localidad>), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer localidades por Nombre")]
    public async Task<IActionResult> TraerPorNombre(string name)
    {
        var localidades = await _context.Localidad
            .Where(l => l.Nombre.Contains(name))
            .ToListAsync();

        if (!localidades.Any())
        {
            return NotFound(new { message = $"No se encontraron localidades con el nombre '{name}'." });
        }

        return Ok(localidades);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Localidad>), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer todas las localidades")]
    public async Task<IActionResult> TraerTodos()
    {
        var localidades = await _context.Localidad.ToListAsync();
        if (!localidades.Any())
        {
            return NotFound(new { message = "No se encontraron localidades registradas." });
        }

        return Ok(localidades);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [SwaggerOperation(Summary = "Actualizar una localidad")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Localidad data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var localidad = await _context.Localidad.FindAsync(id);
        if (localidad == null)
        {
            return NotFound(new { message = $"La localidad con ID {id} no fue encontrada." });
        }

        try
        {
            localidad.Nombre = data.Nombre;
            localidad.CodigoPostal = data.CodigoPostal;
            localidad.FechaAlta = data.FechaAlta;
            localidad.FechaBaja = data.FechaBaja;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al actualizar la localidad con ID {id}.");
            return BadRequest($"Error actualizando el registro. Detalle: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Eliminar una localidad")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var localidad = await _context.Localidad.FindAsync(id);
        if (localidad == null)
        {
            return NotFound(new { message = $"La localidad con ID {id} no fue encontrada." });
        }

        try
        {
            _context.Localidad.Remove(localidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al eliminar la localidad con ID {id}.");
            return BadRequest($"Error eliminando el registro. Detalle: {ex.Message}");
        }
    }
}
