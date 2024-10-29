using app_inventario_andres_navarro.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace app_inventario_andres_navarro
{
    /// <summary>
    /// Lógica de interacción para Agregarproducto.xaml
    /// </summary>
    public partial class Agregarproducto : Window
    {
        private ObservableCollection<Producto> productoList;
        private Conexion conexion;

        public Agregarproducto(ObservableCollection<Producto> productos, Conexion conexion)
        {
            InitializeComponent();
            this.productoList = productos;
            this.conexion = conexion;
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            //Verificar que no esten vacíos los campos
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor completa todos los campos.");
                return; // No continúa si hay campos vacíos
            }

            int idProducto;
            if (!int.TryParse(txtId.Text, out idProducto))
            {
                MessageBox.Show("La ID debe ser un número entero");
                return;
            }

            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("La Cantidad debe ser un número entero");
                return;
            }

            //Verifica si el producto con la ID ya existe
            if (conexion.existeID(idProducto))
            {
                MessageBox.Show("La ID del producto ya existe.");
                return;
            }


            var nuevoProducto = new Producto
            {
                IdProducto = idProducto,
                NombreProducto = txtNombre.Text,
                Cantidad = cantidad,
                Precio = txtPrecio.Text,
                Descripcion = txtDescripcion.Text
            };

          
            conexion.AddProducto(nuevoProducto);

            productoList.Add(nuevoProducto);
            MessageBox.Show("Producto agregado con éxito!");            
            Close(); // Cerrar la ventana después de agregar
        }
    }
}
