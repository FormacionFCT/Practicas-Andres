using app_inventario_andres_navarro.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Editar : Window
    {

        private Producto productoSeleccionado;
        private ObservableCollection<Producto> productoList;
        private Conexion conexion;

        public Editar(Producto producto, ObservableCollection<Producto> productos)
        {
            InitializeComponent();

            this.productoSeleccionado = producto;
            this.productoList = productos;
            this.conexion = new Conexion();

            // Precargo los datos del empleado en los TextBox
            txtId.Text = productoSeleccionado.IdProducto.ToString();
            txtNombre.Text = productoSeleccionado.NombreProducto.ToString();
            txtCantidad.Text = productoSeleccionado.Cantidad.ToString();
            txtPrecio.Text = productoSeleccionado.Precio.ToString();
            txtDescripcion.Text = productoSeleccionado.Descripcion.ToString();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {

            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("La Cantidad debe ser un número entero");
                return;
            }

            // Convertir y validar Precio
            string precioInput = txtPrecio.Text.Replace(',', '.'); // Reemplazar la coma por un punto
            float precio;
            if (!float.TryParse(precioInput, NumberStyles.Any, CultureInfo.InvariantCulture, out precio))
            {
                MessageBox.Show("El precio debe ser un número decimal válido");
                return;
            }

            // Actualizar los datos del empleado seleccionado
            productoSeleccionado.NombreProducto = txtNombre.Text;
            productoSeleccionado.Cantidad = int.Parse(txtCantidad.Text);
            productoSeleccionado.Precio = float.Parse(txtPrecio.Text);
            productoSeleccionado.Descripcion = txtDescripcion.Text;            

            conexion.Editar(productoSeleccionado);

            MessageBox.Show("Producto editado con éxito!");
            Close(); // Cerrar la ventana después de editar
        }

    }
}


