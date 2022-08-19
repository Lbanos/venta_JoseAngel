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

        [HttpPut]
        public async Task<ActionResult<string>> UpdateProducto(Productos producto)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            string mensaje;

            try
            {
                cmd.CommandText = "updateProducto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", producto.Id);
                cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("Precio", producto.Precio);

                cmd.Connection = conn;

                await conn.OpenAsync();

                int i = await cmd.ExecuteNonQueryAsync();

                if (i > 0)
                {
                    mensaje = "Producto Editado con Exito";
                }
                else
                {
                    mensaje = "No hay un producto con el Id proporsionado";
                }






            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return mensaje;
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteProducto(int Id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            string mensaje;

            try
            {
                cmd.CommandText = "deleteProducto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Id);
               

                cmd.Connection = conn;

                await conn.OpenAsync();

                int i = await cmd.ExecuteNonQueryAsync();

                if (i > 0)
                {
                    mensaje = "Producto Eliminado con Exito";
                }
                else
                {
                    mensaje = "No hay un producto con el Id proporsionado";
                }






            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return mensaje;
        }
    }
}
