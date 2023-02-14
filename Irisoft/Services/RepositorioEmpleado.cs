using Dapper;
using Irisoft.Models;
using Irisoft.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace Irisoft.Services
{

    public class RepositorioEmpleado : IRepositorioEmpleado
    {

        private readonly string connectionString;

        public RepositorioEmpleado(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Empleado empleado)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT into Empleados (EmpleadoNombre) VALUES (@EmpleadoNombre);
                                                        SELECT SCOPE_IDENTITY();", empleado);

            empleado.Id = id;
        }


        public async Task<bool> Existe(string EmpleadoNombre)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                                @"SELECT 1 
                                                FROM Empleados 
                                                WHERE EmpleadoNombre = @EmpleadoNombre;", new { EmpleadoNombre });

            return existe == 1;
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodos()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Empleado>(@"SELECT Id, EmpleadoNombre FROM Empleados");
        }

        public async Task Actualizar (Empleado empleado)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Empleados SET EmpleadoNombre = @EmpleadoNombre WHERE Id = @Id", empleado);
        }

        public async Task<Empleado> ObtenerEntidadPorId (int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Empleado>(@"SELECT Id, EmpleadoNombre FROM Empleados WHERE Id = @Id", new { id });
        }

        public async Task Borrar (int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Empleados WHERE Id = @Id", new { id });
            
        }

    }

}

