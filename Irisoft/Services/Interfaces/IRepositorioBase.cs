using Irisoft.Models;

namespace Irisoft.Services.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        Task Borrar(int id);
        Task Crear(T entidad);
        Task<bool> Existe(string nombre);
        Task<T> ObtenerEntidadPorId(int id);
        Task<IEnumerable<T>> ObtenerTodos();
        Task Actualizar(T entidad);
    }

}

