using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_inventario_andres_navarro.Modelos
{
    public class ViewModel
    {
        public ObservableCollection<Producto> Productos { get; set; }


        public ViewModel() 
        {
            Productos = new ObservableCollection<Producto>
            {
                new()
              {

                    IdProducto = 1,
                    NombreProducto = "Mesa",
                    Cantidad = 10,
                    Precio = "50,20$",
                    Descripcion = "Mesas de auténtico roble"
                },

                 new() {

                     IdProducto = 2,
                     NombreProducto = "Silla",
                     Cantidad = 4,
                     Precio = "10$",
                     Descripcion = "Sillas plegables"
                 },
                 new() {

                     IdProducto = 3,
                     NombreProducto = "Armario",
                     Cantidad = 5,
                     Precio = "100$",
                     Descripcion = "Armarios de 2m de altura con amplio fondo"
                 }
            };
        }
    }
}
