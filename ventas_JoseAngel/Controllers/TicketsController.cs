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
    public class TicketsController : ControllerBase
    {
        string connectionString = "Server=DESKTOP-3UCSVA6\\SQLEXPRESS;Database=ventas_JoseAngel;Trusted_Connection=True";

        [HttpGet]
        public async Task<ActionResult<string>> GetLastTicket()
        {
           
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            string ticket;

            try
            {
                cmd.CommandText = "getLastTicket";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                await conn.OpenAsync();

                ticket = Convert.ToString(await cmd.ExecuteScalarAsync());

                





            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return $"El ultimo ticket es {ticket}";
        }
    }
}
