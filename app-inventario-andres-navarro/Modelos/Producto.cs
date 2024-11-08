using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_inventario_andres_navarro.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public string Descripcion { get; set; }

       
    }
}
