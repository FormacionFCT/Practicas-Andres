using app_inventario_andres_navarro.Modelos;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace app_inventario_andres_navarro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Declaro el ViewModel como campo de clase
        private ViewModel viewModel;


        public MainWindow()
        {

            InitializeComponent();

            // Instanciamos el ViewModel y lo asignamos como DataContext
            viewModel = new ViewModel();
            this.DataContext = viewModel;
        }

        // Método filtrar búsqueda
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            //Obtengo el texto del TexBox
            string textoBusqueda = txtBusqueda.Text.ToLower();

            //Filtrado
            var productoFiltrados = viewModel.Productos.Where(emp => emp.NombreProducto.ToLower().Contains(textoBusqueda)).ToList();

            //Actualizo el DataGRid
            DataGridXAML.ItemsSource = null;
            DataGridXAML.ItemsSource = productoFiltrados;
        }

        // Crear un nuevo producto
        private void CrearProducto(object sender, RoutedEventArgs e)
        {
            // Añadimos un nuevo empleado
            Agregarproducto agregarproducto = new Agregarproducto(viewModel.Productos, viewModel._conexion);
            agregarproducto.ShowDialog(); // Muestra la ventana como un diálogo modal
                        
            DataGridXAML.Items.Refresh();

            // Deseleccionar el empleado 
            DataGridXAML.SelectedItem = null;
        }


        // Leer (Seleccionar) un empleado
        private void Leer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem is Producto productoSeleccionado)
            {
                MessageBox.Show($"ID: {productoSeleccionado.IdProducto}\n" +
                                $"Nombre: {productoSeleccionado.NombreProducto}\n" +
                                $"Dirección: {productoSeleccionado.Cantidad}\n" +
                                $"Ciudad: {productoSeleccionado.Precio}\n" +
                                $"País: {productoSeleccionado.Descripcion}", "Detalles del Producto");

                DataGridXAML.Items.Refresh();

                // Deseleccionar el empleado 
                DataGridXAML.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Por favor selecciona un empleado.");
            }
        }

        // Actualizar un producto seleccionado
        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridXAML.SelectedItem is Producto productoSeleccionado)
            {
                Editar editarWindow = new Editar(productoSeleccionado, viewModel.Productos);
                editarWindow.ShowDialog();

                DataGridXAML.Items.Refresh();

                // Deseleccionar el empleado 
                DataGridXAML.SelectedItem = null;
            }

            else
            {
                MessageBox.Show("Por favor selecciona un producto.");
            }
        }


        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridXAML.SelectedItem is Producto productoSeleccionado)
            {
                // Mostrar un mensaje de confirmación
                MessageBoxResult resultado = MessageBox.Show(
                    $"¿Estás seguro de que quieres eliminar el producto {productoSeleccionado.NombreProducto}?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                // Si el usuario selecciona "Sí", procedemos con la eliminación
                if (resultado == MessageBoxResult.Yes)
                {
                    viewModel.EliminarProducto(productoSeleccionado);
                    DataGridXAML.Items.Refresh();

                    // Deseleccionar el empleado después de eliminar
                    DataGridXAML.SelectedItem = null;
                }

            }
        }
    }
}