using System.Data.SQLite;
using CadenasDeconexion;
using Models;
using ViewModels;

namespace Repositorios
{
    public class RepositorioCadeteSQLite : IRepositorioCadete
    {

        private readonly string _cadenaDeConexion;

        public RepositorioCadeteSQLite(ICadenaDeConexion cadenaDeConexion)
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

        private static Cadete CrearCadete(SQLiteDataReader? reader)
        {
            var nuevo = new Cadete();
            nuevo.Id = Convert.ToInt32(reader["id_cadete"]); //controles de rader != null && any?
            nuevo.Nombre = reader["cadete"].ToString();
            nuevo.Telefono = reader["telefono"].ToString();
            nuevo.Direccion = reader["direccion"].ToString();
            return nuevo;
        }

        public int? GetLastId()
        {
            try
            {
                int idNuevo;
                var connection = GetConnection();
                var queryString = $"SELECT max(id_cadete) FROM cadete;";
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

        public Cadete? GetById(int? id)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"SELECT * FROM cadete WHERE id_cadete = {id};";
                var comando = new SQLiteCommand(queryString, connection);
                // Cadete nuevo;
                Cadete? nuevo = null;
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nuevo = CrearCadete(reader);
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

        /*Optimizar consultas? en vez de hacer una consulta por cada cadete para recuperar el nro de
        pedidos pendientes, podría hacer una sola consulta y guardar el número en un objeto 
        de una nueva clase "CadetesConPedidos?" 
        var queryString = "SELECT cadete.*, count(id_pedido) FROM  cadete LEFT JOIN (SELECT id_pedido, id_cadete FROM pedido WHERE estado = 'Viajando') USING(id_cadete) GROUP BY id_cadete;";*/
        public List<Cadete>? GetAll()
        {
            try
            {
                var listaCadetes = new List<Cadete>();
                var connection = GetConnection();
                var queryString = "SELECT * FROM cadete;";
                var comando = new SQLiteCommand(queryString, connection);
                using (var reader = comando.ExecuteReader())
                {
                    Cadete nuevo;
                    while (reader.Read())
                    {
                        nuevo = CrearCadete(reader);
                        listaCadetes.Add(nuevo);
                    }
                }
                connection.Close();
                return listaCadetes;
            }
            catch (Exception ex)
            {
                //Nlog
                Console.WriteLine("mostrar cadetes error");
                Console.WriteLine(ex);
                return null;
            }

        }

        public void Save(Cadete cadete)
        {
            try
            {
                string? nombre = cadete.Nombre;
                string? direccion = cadete.Direccion;
                string? telefono = cadete.Telefono;
                var connection = GetConnection();
                var queryString = $"INSERT INTO cadete(cadete, direccion, telefono, id_cadeteria) VALUES ('{nombre}', '{direccion}', '{telefono}', 1);";
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

        public void Update(Cadete cadete)
        {
            try
            {
                string? nombre = cadete.Nombre;
                string? direccion = cadete.Direccion;
                string? telefono = cadete.Telefono;
                var connection = GetConnection();
                var queryString = $"UPDATE cadete SET cadete = '{nombre}', direccion = '{direccion}', telefono = '{telefono}' WHERE id_cadete = {cadete.Id};";
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
                var queryString = $"DELETE FROM cadete WHERE id_cadete = {id};";
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

        public void AsignarPedido(AsignarPedidoViewModel asignar)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"UPDATE pedido SET id_cadete = {asignar.IdCadete}, estado = 'Viajando' WHERE id_pedido = {asignar.IdPedido};";
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







    }
}