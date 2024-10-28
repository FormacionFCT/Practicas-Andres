using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app_inventario_andres_navarro.Modelos
{
    public class ViewModel
    {
        public ObservableCollection<Producto> Productos { get; set; }
        public Conexion _conexion;


        public ViewModel() 
        {
            _conexion = new Conexion();

            var listaProductos = _conexion.GetAllProducto();
            Productos = new ObservableCollection<Producto>(listaProductos);

        }

        public void AgregarProducto(Producto nuevoProducto)
        {
            _conexion.AddProducto(nuevoProducto);

            Productos.Add(nuevoProducto);
        }

        public void EditarEmpleado(Producto producto)
        {
            bool exito = _conexion.Editar(producto);

            if (exito)
            {

                var productoExistente = Productos.FirstOrDefault(e => e.IdProducto == producto.IdProducto);
                if (productoExistente != null)
                {
                    productoExistente.NombreProducto = producto.NombreProducto;
                    productoExistente.Cantidad = producto.Cantidad;
                    productoExistente.Precio = producto.Precio;
                    productoExistente.Descripcion = producto.Descripcion;

                    var index = Productos.IndexOf(productoExistente);
                    Productos[index] = productoExistente;
                }
            }
            else
            {
                MessageBox.Show("Error al actualizar el empleado en la base de datos.");
            }
        }

        // Método para eliminar un empleado
        public void EliminarEmpleado(Producto producto)
        {
            bool exito = _conexion.Eliminar(producto);
            if (exito)
            {
               Productos.Remove(producto);
            }
            else
            {
                MessageBox.Show("Error al eliminar el empleado en la base de datos.");
            }
        }


    }
}