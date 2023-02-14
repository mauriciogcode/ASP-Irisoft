using Dapper;
using Irisoft.Controllers;
using Irisoft.Models;
using Microsoft.Data.SqlClient;

namespace Irisoft.Services
{
    public interface IVentaDiariaRepositorio
    {
        Task Crear(VentaDiaria ventaDiaria);
    }
    public class VentaDiariaRepositorio : IVentaDiariaRepositorio
    {
        private readonly string connectionString;

        public VentaDiariaRepositorio(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task Crear (VentaDiaria ventaDiaria)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO VentasDiarias 
           (Orden, ClienteNombre, Accesorio, Cantidad, IdTipoCristal, IdLaboratorio, OrdenLaboratorio,
            Armazon, RecibeNombre, IdEmpleado, IdTarjeta, Cuotas, MontoSinRecibo,
           MontoEfectivo, MontoTarjeta, MontoTransferencia, MontoTotal)
     VALUES
           (1, @ClienteNombre, 1, @Cantidad, @IdTipoCristal, @IdLaboratorio, @OrdenLaboratorio,
            @Armazon, @RecibeNombre, 1, 1, 1, 1, 
            1, 1, 1, 1); 
            SELECT SCOPE_IDENTITY();", ventaDiaria);


            ventaDiaria.Id = id;
        }
    }

 
}
