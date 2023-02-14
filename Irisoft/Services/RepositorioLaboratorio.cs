using Dapper;
using Irisoft.Models;
using Irisoft.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace Irisoft.Services
{
    public class RepositorioLaboratorio : IRepositorioLaboratorio
    {
        private readonly string connectionString;

        public RepositorioLaboratorio(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        
        public async Task Crear(Laboratorio laboratorio)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT into Laboratorios (Nombre) VALUES (@Nombre); SELECT SCOPE_IDENTITY()", laboratorio);
          
            laboratorio.Id = id;
        }

        public async Task<bool> Existe(string laboratorioNombre) {
            using var connection = new SqlConnection(connectionString);
        var existe = await connection.QueryFirstOrDefaultAsync<int>(
            @"SELECT 1  FROM Laboratorios
WHERE Nombre = @laboratorioNombre;", new { laboratorioNombre });

            return existe == 1;
        }

        public async Task<IEnumerable<Laboratorio>> ObtenerTodos()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Laboratorio>(@"SELECT Id, Nombre FROM Laboratorios");
        }


        public async Task Actualizar (Laboratorio laboratorio)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Laboratorios SET Nombre = @Nombre WHERE Id = @Id", laboratorio);
        }

        public async Task<Laboratorio> ObtenerEntidadPorId (int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Laboratorio>(@"SELECT Id, Nombre FROM Laboratorios WHERE Id = @Id", new { id });
        }

        public async Task Borrar (int id)
        {
            using var connection = new SqlConnection(connectionString);
          await connection.ExecuteAsync(@"DELETE Laboratorios WHERE Id = @Id", new { id });
        }
    }


}
