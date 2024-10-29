using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app_inventario_andres_navarro.Modelos
{
    public class Conexion
    {
        private string conexionString = "Data Source=productos.db;Version=3;";

        public Conexion()
        {
            CrearTablaSiNoExiste(); // Llama al método en el constructor
        }

        // Método que crea la tabla si no existe
        private void CrearTablaSiNoExiste()
        {
            using (var conexion = new SQLiteConnection(conexionString))
            {
                conexion.Open();

                //SQL para crea tabla
                string query = @"
                CREATE TABLE IF NOT EXISTS Producto (
                    Id INTEGER PRIMARY KEY UNIQUE,
                    Nombre TEXT NOT NULL,
                    Cantidad INTEGER NOT NULL,
                    Precio TEXT NOT NULL,
                    Descripcion TEXT NOT NULL
                );";
                using (var command = new SQLiteCommand(query, conexion))
                {
                    command.ExecuteNonQuery(); // Ejecuta el comando
                }
            }
        }

        public List<Producto> GetAllProducto()
        {
            List<Producto> oLista = new List<Producto>();

            using (var connection = new SQLiteConnection(conexionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Cantidad, Precio, Descripcion FROM Producto";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Producto()
                            {
                                IdProducto = int.Parse(dr["Id"].ToString()),
                                NombreProducto = dr["Nombre"].ToString(),
                                Cantidad = int.Parse(dr["Cantidad"].ToString()),
                                Precio = dr["Precio"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }
                }
            }

            return oLista;
        }

        public bool Editar(Producto obj)
        {
            bool respuesta = true;



            using (var connection = new SQLiteConnection(conexionString))
            {
                connection.Open();
                string query = "Update Producto set Nombre = @nombre, Cantidad = @cantidad, Precio = @precio, Descripcion = @descripcion where Id = @id";

                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.Add(new SQLiteParameter("@id", obj.IdProducto));
                cmd.Parameters.Add(new SQLiteParameter("@nombre", obj.NombreProducto));
                cmd.Parameters.Add(new SQLiteParameter("@cantidad", obj.Cantidad));
                cmd.Parameters.Add(new SQLiteParameter("@precio", obj.Precio));
                cmd.Parameters.Add(new SQLiteParameter("@descripcion", obj.Descripcion));
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.ExecuteNonQuery() < 1)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }


        public void AddProducto(Producto producto)
        {
            using (var connection = new SQLiteConnection(conexionString))
            {
                connection.Open();
                string query = "INSERT INTO Producto (Id, Nombre, Cantidad, Precio, Descripcion) VALUES (@id, @nombre, @cantidad, @precio, @descripcion)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", producto.IdProducto);
                    command.Parameters.AddWithValue("@nombre", producto.NombreProducto);
                    command.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                    command.Parameters.AddWithValue("@precio", producto.Precio);
                    command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Eliminar(Producto obj)
        {
            bool respuesta = true;

            using (var connection = new SQLiteConnection(conexionString))
            {
                connection.Open();
                string query = "Delete from producto where Id = @id";

                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.Add(new SQLiteParameter("@id", obj.IdProducto));
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.ExecuteNonQuery() < 1)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool existeID(int idProducto)
        {
          
                using (var connection = new SQLiteConnection(conexionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Producto  WHERE Id = @id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id",idProducto);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }
           
        
    }
}
