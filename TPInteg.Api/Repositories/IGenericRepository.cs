using Microsoft.AspNetCore.Mvc;

namespace TPInteg.Api.Repositories
{
    public interface IGenericRepository<in T> where T : class
    {
        Task<IActionResult> TraerTodos();
        Task<IActionResult> TraerPorId(int id);
        Task<IActionResult> TraerPorNombre(string nombre);
        Task<IActionResult> Crear([FromBody] T data);
        Task<IActionResult> Eliminar(int id);
        Task<IActionResult> Actualizar([FromBody] T data);
    }
}
