using Dapper;
using Irisoft.Models;
using Irisoft.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace Irisoft.Services
{
    public class RepositorioTarjeta : IRepositorioTarjeta
    {
        private readonly string connectionString;

        public RepositorioTarjeta(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task Crear(Tarjeta Tarjeta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT into Tarjetas (Nombre) VALUES (@Nombre); SELECT SCOPE_IDENTITY()", Tarjeta);

            Tarjeta.Id = id;
        }

        public async Task<bool> Existe(string TarjetaNombre)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1  FROM Tarjetas
WHERE Nombre = @TarjetaNombre;", new { TarjetaNombre });

            return existe == 1;
        }

        public async Task<IEnumerable<Tarjeta>> ObtenerTodos()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Tarjeta>(@"SELECT Id, Nombre FROM Tarjetas");
        }


        public async Task Actualizar(Tarjeta Tarjeta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Tarjetas SET Nombre = @Nombre WHERE Id = @Id", Tarjeta);
        }

        public async Task<Tarjeta> ObtenerEntidadPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Tarjeta>(@"SELECT Id, Nombre FROM Tarjetas WHERE Id = @Id", new { id });
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Tarjetas WHERE Id = @Id", new { id });
        }
    }


}
