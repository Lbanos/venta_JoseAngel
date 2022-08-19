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
    public class ProductosController : ControllerBase
    {
        string connectionString = "Server=DESKTOP-3UCSVA6\\SQLEXPRESS;Database=ventas_JoseAngel;Trusted_Connection=True";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            List<Productos> productos = new List<Productos>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Productos producto;

            try
            {
                cmd.CommandText = "getProductos";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                await conn.OpenAsync();

                dr = await cmd.ExecuteReaderAsync();

                while (dr.Read())
                {
                    producto = new Productos();

                    producto.Id = dr.GetInt32(0);
                    producto.Nombre = dr.GetString(1);
                    producto.Descripcion = dr.GetString(2);
                    producto.Precio = dr.GetDecimal(3);

                    productos.Add(producto);
                }



            

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return productos;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateProducto(Productos nuevoProducto)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            

            try
            {
                cmd.CommandText = "createProducto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", nuevoProducto.Nombre);
                cmd.Parameters.AddWithValue("Descripcion", nuevoProducto.Descripcion);
                cmd.Parameters.AddWithValue("Precio", nuevoProducto.Precio);

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
            return "Producto registrado con exito!!";
        }
    }
}
