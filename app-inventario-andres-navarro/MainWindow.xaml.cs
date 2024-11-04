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