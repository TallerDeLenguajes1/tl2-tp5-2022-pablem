using System.Data.SQLite;
using CadenasDeconexion;
using Models;
// using ViewModels;

namespace Repositorios
{
    public class RepositorioClienteSQLite : IRepositorioCliente
    {
        private readonly string _cadenaDeConexion;

        public RepositorioClienteSQLite(ICadenaDeConexion cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion.GetCadena();
        }

        private SQLiteConnection GetConnection()
        {
            // var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(_cadenaDeConexion);
            connection.Open();
            return connection;
        }

        public static Cliente CrearCliente(SQLiteDataReader reader)
        {
            Cliente nuevo = new Cliente();
            nuevo.Id = Convert.ToInt32(reader["id_cliente"]);
            nuevo.Nombre = reader["cliente"].ToString();
            nuevo.Telefono = reader["telefono"].ToString();
            nuevo.Direccion = reader["direccion"].ToString();
            if (!reader.IsDBNull(reader.GetOrdinal("referencia_direccion")))
                nuevo.ReferenciaDireccion = reader["referencia_direccion"].ToString()!;
            return nuevo;
        }

        public int? ProxId()
        {
            try
            {
                int idNuevo;
                var connection = GetConnection();
                var queryString = $"SELECT max(id_cliente)+1 FROM cliente;";
                var comando = new SQLiteCommand(queryString, connection);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
                connection.Close();
                return idNuevo;
            }
            catch (Exception ex)
            {
                //Nlog 
                Console.WriteLine(ex);
                return null;
            }
        }

        public Cliente? GetById(int? id)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"SELECT * FROM cliente WHERE id_cliente = {id};";
                var comando = new SQLiteCommand(queryString, connection);

                // Cliente nuevo;
                Cliente? nuevo = null;
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nuevo = CrearCliente(reader);
                    }
                }
                connection.Close();
                return nuevo;
            }
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("get cadeteid error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<Cliente>? GetAll()
        {
            try
            {
                var listaClientes = new List<Cliente>();
                var connection = GetConnection();
                var queryString = "SELECT * FROM cliente ORDER BY id_cliente DESC;";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    Cliente nuevo;
                    while (reader.Read())
                    {
                        nuevo = CrearCliente(reader);
                        listaClientes.Add(nuevo);
                    }
                }
                connection.Close();
                return listaClientes;
            }
            catch (Exception ex)
            {
                //Nlog
                Console.WriteLine("obtener todos los clientes error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public void Save(Cliente cliente)
        {
            try
            {
                string? nombre = cliente.Nombre;
                string? direccion = cliente.Direccion;
                string? telefono = cliente.Telefono;
                string? refDireccion = (cliente.ReferenciaDireccion == null || cliente.ReferenciaDireccion == "") ? "null" : "'" + cliente.ReferenciaDireccion + "'";
                var connection = GetConnection();
                var queryString = $"INSERT INTO cliente(cliente, direccion, telefono, referencia_direccion ) VALUES ('{nombre}', '{direccion}', '{telefono}', {refDireccion});";
                var comando = new SQLiteCommand(queryString, connection);
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //N
                Console.WriteLine(ex);
            }
        }

        public void Update(Cliente cliente)
        {
            try
            {
                int id = cliente.Id;
                string? nombre = cliente.Nombre;
                string? telefono = cliente.Telefono;
                string? direccion = cliente.Direccion;
                string? refDireccion = (cliente.ReferenciaDireccion == null || cliente.ReferenciaDireccion == "") ? "null" : "'" + cliente.ReferenciaDireccion + "'";
                var connection = GetConnection();
                var queryString = $"UPDATE cliente SET cliente = '{nombre}', telefono = '{telefono}', direccion = '{direccion}', referencia_direccion = {refDireccion} WHERE id_cliente = {id};";
                var comando = new SQLiteCommand(queryString, connection);
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //N
                Console.WriteLine(ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"DELETE FROM cliente WHERE id_cliente = {id};";
                var comando = new SQLiteCommand(queryString, connection);
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("delete id error");
                Console.WriteLine(ex);
                //throw;
            }
        }








    }
}