using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ventas_JoseAngel.Modelos;

namespace ventas_JoseAngel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        string connectionString = "Server=DESKTOP-3UCSVA6\\SQLEXPRESS;Database=ventas_JoseAngel;Trusted_Connection=True";

        [HttpPost]
        public async Task<ActionResult<string>> RegistrarVenta(Venta nuevaVenta)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();


            try
            {
                cmd.CommandText = "registrarVenta";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id_Producto", nuevaVenta.Id_Producto);
                cmd.Parameters.AddWithValue("Ticket", nuevaVenta.Ticket);
                cmd.Parameters.AddWithValue("CantidadProducto", nuevaVenta.CantidadProducto);


                cmd.Connection = conn;

                await conn.OpenAsync();

                await cmd.ExecuteNonQueryAsync();







            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return "Venta realizada con exito!!";
        }
    }
}
