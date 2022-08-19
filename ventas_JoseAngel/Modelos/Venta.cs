using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ventas_JoseAngel.Modelos
{
    public class Venta
    {
        public int Id { get; set; }
        public int Id_Producto { get; set; }
        public string Ticket { get; set; }
        public decimal CantidadProducto { get; set; }
        public decimal TotalProductos { get; set; } 

        public Venta() { }
    }
}
