using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TPInteg.Persistance;
using TPInteg.Shared;

namespace TPInteg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductoController : ControllerBase
{
    private readonly TPIntegradorDbContext _context;
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(TPIntegradorDbContext context, ILogger<ProductoController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), 201)]
    [ProducesResponseType(400)]
    [SwaggerOperation(Summary = "Crear nuevo producto")]
    public async Task<IActionResult> Crear([FromBody] Producto producto)
    {
        try
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(TraerPorId), new { id = producto.Id }, producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el producto.");
            return BadRequest($"Error creando el registro. Detalle: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Producto), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer un producto por ID")]
    public async Task<IActionResult> TraerPorId(int id)
    {
        var producto = await _context.Producto
            .Include(p => p.Proveedor)
            .FirstOrDefaultAsync(p => p.Id == id);
        return producto == null ? NotFound() : Ok(producto);
    }

    [HttpGet("nombre/{name}")]
    [ProducesResponseType(typeof(List<Producto>), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer productos por Nombre")]
    public async Task<IActionResult> TraerPorNombre(string name)
    {
        var productos = await _context.Producto
            .Where(p => p.Descripcion.Contains(name))
            .ToListAsync();
        return productos.Any() ? Ok(productos) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Producto>), 200)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Traer todos los productos")]
    public async Task<IActionResult> TraerTodos()
    {
        var productos = await _context.Producto.Include(p => p.Proveedor).ToListAsync();
        return productos.Any() ? Ok(productos) : NotFound();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Actualizar un producto")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Producto data)
    {
        try
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null) return NotFound();

            // Actualizar todos los campos necesarios
            producto.Codigo = data.Codigo;
            producto.Descripcion = data.Descripcion;
            producto.PrecioUnitario = data.PrecioUnitario;
            producto.Stock = data.Stock;
            producto.ProveedorId = data.ProveedorId;

            // Manejar el estado y las fechas
            if (data.Estado?.ToLower() == "inactivo")
            {
                producto.FechaBaja = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                producto.FechaBaja = null;
                producto.Estado = "activo";
            }

            // Si la fecha de alta viene en el request, la actualizamos
            if (data.FechaAlta.HasValue)
            {
                producto.FechaAlta = data.FechaAlta.Value;
            }

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el producto {Id}", id);
            return BadRequest($"Error actualizando el producto. Detalle: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [SwaggerOperation(Summary = "Eliminar un producto")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var producto = await _context.Producto.FindAsync(id);
        if (producto == null) return NotFound();

        _context.Producto.Remove(producto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

