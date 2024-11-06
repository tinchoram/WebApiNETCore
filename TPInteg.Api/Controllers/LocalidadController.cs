using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TPInteg.Api.Repositories;
using TPInteg.Persistance;
using TPInteg.Shared;

namespace TPInteg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LocalidadController : ControllerBase, ILocalidadRepository
{
    private readonly ILogger<ProveedorController> _logger;
    private readonly TPIntegradorDbContext _tPIntegradorDbContext;
    public LocalidadController(ILogger<ProveedorController> logger, TPIntegradorDbContext tPIntegradorDbContext)
    {
        _logger = logger;
        _tPIntegradorDbContext = tPIntegradorDbContext;
    }

    [HttpPatch]
    [ProducesResponseType(typeof(bool), 204)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Recibe un localidad para actualizar")]
    [SwaggerOperation(Summary = "Actualizar un localidad de manera parcial")]
    public async Task<IActionResult> Actualizar([FromBody] Localidad data)
    {
        try
        {
            var localidadId = data.Id;
            var localidad = await _tPIntegradorDbContext.Localidad
                .FirstOrDefaultAsync(x => x.Id == localidadId);
            if (localidad == null)
            {
                return NotFound();
            }
            localidad.Nombre = data.Nombre;
            localidad.FechaAlta = data.FechaAlta;
            localidad.FechaBaja = data.FechaBaja;
            await _tPIntegradorDbContext.SaveChangesAsync();
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error actualizando el registro. Detalle:{ex.Message}");
        }
    }
    [HttpPost]
    [ProducesResponseType(typeof(int), 201)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 409)]
    [EndpointDescription("Recibe un localidad para crear")]
    [SwaggerOperation(Summary = "Crear nuevo localidad")]
    public async Task<IActionResult> Crear([FromBody] Localidad data)
    {
        try
        {
            var nuevaLocalidad = await _tPIntegradorDbContext.Localidad.AddAsync(data);
            await _tPIntegradorDbContext.SaveChangesAsync();
            return Ok(nuevaLocalidad.Entity.Id);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creando el registro. Detalle:{ex.Message}");
        }
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Recibe un Id de localidad para eliminar")]
    [SwaggerOperation(Summary = "Eliminar un localidad de manera lógica")]
    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            var localidadId = id;
            var localidad = await _tPIntegradorDbContext.Localidad
                .FirstOrDefaultAsync(x => x.Id == localidadId);
            if (localidad == null)
            {
                return NotFound();
            }
            //BAJA FISICA
            _tPIntegradorDbContext.Localidad.Remove(localidad);
            //BAJA LOGICA
            //localidad.FechaBaja = DateOnly.FromDateTime(DateTime.Now);
            await _tPIntegradorDbContext.SaveChangesAsync();            

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error eliminando el registro. Detalle:{ex.Message}");
        }
    }
    [HttpGet("TraerPorId/{id}")]
    [ProducesResponseType(typeof(Localidad), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Obtener datos de un localidad por su ID")]
    [SwaggerOperation(Summary = "Traer un localidad por ID")]
    public async Task<IActionResult> TraerPorId(int id)
    {
        try
        {
            var localidad = await _tPIntegradorDbContext.Localidad
                .FirstOrDefaultAsync(x => x.Id == id);
            if (localidad == null) 
            {
                return NotFound();
            }
            return Ok(localidad);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error devolviendo el registro. Detalle:{ex.Message}");
        }
    }
    [HttpGet("TraerPorNombre/{nombre}")]
    [ProducesResponseType(typeof(List<Localidad>), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Obtener datos de las localidades que contengan un Nombre")]
    [SwaggerOperation(Summary = "Traer localidades por Nombre")]
    public async Task<IActionResult> TraerPorNombre(string nombre)
    {
        try
        {
            var listaLocalidades = await _tPIntegradorDbContext.Localidad
                .Where(x => x.Nombre.Contains(nombre)).ToListAsync();
            if (listaLocalidades is null)
            {
                return NotFound();
            }
            return Ok(listaLocalidades);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error devolviendo el registro. Detalle:{ex.Message}");
        }
    }
    [HttpGet("TraerTodos")]
    [ProducesResponseType(typeof(List<Localidad>), 200)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Obtener datos de todos las localidades")]
    [SwaggerOperation(Summary = "Traer todas las localidades")]
    //[Authorize(Roles = "Usuario")]
    public async Task<IActionResult> TraerTodos()
    {
        try
        {
            var listaLocalidades = await _tPIntegradorDbContext.Localidad.ToListAsync();
            if (listaLocalidades is null)
            {
                return NotFound();
            }
            return Ok(listaLocalidades);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error devolviendo el registro. Detalle:{ex.Message}");
        }
    }
    [HttpPut("ActualizarFullLocalidad")]
    [ProducesResponseType(typeof(bool), 204)]
    [ProducesResponseType(typeof(int), 400)]
    [ProducesResponseType(typeof(int), 404)]
    [EndpointDescription("Recibe una localidad para actualizar")]
    [SwaggerOperation(Summary = "Actualiza una localidad de manera completa")]
    public async Task<IActionResult> ActualizarFullLocalidad([FromBody] Localidad data) 
    {
        try
        {
            var localidadId = data.Id;
            var localidad = await _tPIntegradorDbContext.Localidad
                .FirstOrDefaultAsync(x => x.Id == localidadId);
            if (localidad == null)
            {
                return NotFound();
            }
            localidad.Nombre = data.Nombre;
            await _tPIntegradorDbContext.SaveChangesAsync();
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error actualizando el registro. Detalle:{ex.Message}");
        }
    }
}
