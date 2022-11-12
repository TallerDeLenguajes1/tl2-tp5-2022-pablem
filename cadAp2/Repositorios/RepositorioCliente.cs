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
                var queryString = "SELECT * FROM cliente;";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    Cliente nuevo;
                    while (reader.Read())
                    {
                        nuevo = new Cliente();
                        nuevo.Id = Convert.ToInt32(reader["id_cliente"]);
                        nuevo.Nombre = reader["cliente"].ToString();
                        nuevo.Telefono = reader["telefono"].ToString();
                        nuevo.Direccion = reader["direccion"].ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("referencia_direccion")))
                            nuevo.ReferenciaDireccion = reader["referencia_direccion"].ToString();
                        listaClientes.Add(nuevo);
                    }
                }
                connection.Close();
                return listaClientes;
            }
            catch(Exception ex)
            {
                //Nlog
                Console.WriteLine("mostrar cadetes error");
                Console.WriteLine(ex);
                return null;
            }
            
        }

       

    }
}