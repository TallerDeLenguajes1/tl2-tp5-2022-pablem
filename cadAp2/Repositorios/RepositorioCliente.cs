using System.Data.SQLite;
using Models;
// using ViewModels;

namespace Repositorios
{
    public class RepositorioClienteSQLite
    {
        private SQLiteConnection GetConnection()
        {
            var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            return connection;
        }

        public List<Cliente>? GetAll()
        {
            try {
                var listaClientes = new List<Cliente>();
                var connection = GetConnection();
                var queryString = "SELECT * FROM cliente ORDER BY id_cliente DESC;";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    Cliente nuevo;
                    while (reader.Read())
                    {
                        nuevo = new Cliente();
                        nuevo.Id = Convert.ToInt32(reader["id_cliente"]);
                        nuevo.Nombre = reader["cliente"].ToString()!;
                        nuevo.Telefono = reader["telefono"].ToString()!;
                        nuevo.Direccion = reader["direccion"].ToString()!;
                        if (!reader.IsDBNull(reader.GetOrdinal("referencia_direccion")))
                            nuevo.ReferenciaDireccion = reader["referencia_direccion"].ToString()!;
                        listaClientes.Add(nuevo);
                    }
                }
                connection.Close();
                return listaClientes;
            }
            catch(Exception ex)
            {
                //Nlog
                Console.WriteLine("obtener todos los clientes error");
                Console.WriteLine(ex);
                return null;
            }
        }

         public void Save(Cliente cliente)
        {
            try {
                string? nombre = cliente.Nombre;
                string? direccion = cliente.Direccion;
                string? telefono = cliente.Telefono;
                string? refDireccion = (cliente.ReferenciaDireccion == null || cliente.ReferenciaDireccion == "") ? "null": "'"+cliente.ReferenciaDireccion+"'";
                var connection = GetConnection();
                var queryString = $"INSERT INTO cliente(cliente, direccion, telefono, referencia_direccion ) VALUES ('{nombre}', '{direccion}', '{telefono}', {refDireccion});";
                var comando = new SQLiteCommand(queryString, connection);
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                //N
                Console.WriteLine(ex);
            }
        }

        public void Update(Cliente cliente)
        {
            try {
                int id = cliente.Id;
                string? nombre = cliente.Nombre;
                string? telefono = cliente.Telefono;
                string? direccion = cliente.Direccion;
                string? refDireccion = (cliente.ReferenciaDireccion == null || cliente.ReferenciaDireccion == "") ? "null": "'"+cliente.ReferenciaDireccion+"'";
                var connection = GetConnection();
                var queryString = $"UPDATE cliente SET cliente = '{nombre}', telefono = '{telefono}', direccion = '{direccion}', referencia_direccion = {refDireccion} WHERE id_cliente = {id};";
                var comando = new SQLiteCommand(queryString, connection);
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                //N
                Console.WriteLine(ex);
            }
        }

       

    }
}