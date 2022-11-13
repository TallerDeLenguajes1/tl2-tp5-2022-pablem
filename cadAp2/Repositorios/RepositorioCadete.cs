using System.Data.SQLite;
using Models;
using ViewModels;

namespace Repositorios
{
    // public interface IRepositorioCadete
    // {
    //     List<Cadete> GetCadetes();
    // }

    public class RepositorioCadeteSQLite// : IRepositorioCadete
    {

        // private readonly IConfiguration config;
        // private readonly string cadenaDeConexion;
        /* programacion funcional: variable inmutable, 
        solo se modifica en el constructor o en la declaracion */
        

        // private readonly string cadenaDeConexion;
        // public RepositorioCadeteSQLite(IConfiguration configuration)
        // {
        //     this.cadenaDeConexion = configuration.GetConnectionString("default");
        // }

        private SQLiteConnection GetConnection()
        {
            var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            return connection;
        }

        // public int ProxId()
        // {
        //     try {
        //         var connection = GetConnection();
        //         var queryString = $"SELECT max(id_cadete)+1 FROM cadete;";
        //         var comando = new SQLiteCommand(queryString, connection);

        //     }
        //     catch(Exception ex)
        //     {
        //         //Nlog console
                
        //     }
        // }

        public Cadete? GetCadete(int? id)
        {
            //var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            try {
                var connection = GetConnection();
                var queryString = $"SELECT * FROM cadete WHERE id_cadete = {id};";
                var comando = new SQLiteCommand(queryString, connection);

                var nuevo = new Cadete();
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nuevo = new Cadete();
                        nuevo.Id = Convert.ToInt32(reader["id_cadete"]);
                        nuevo.Nombre = reader["cadete"].ToString();
                        nuevo.Telefono = reader["telefono"].ToString();
                        nuevo.Direccion = reader["direccion"].ToString();
                    }
                }
                connection.Close();
                return nuevo;
            }
            catch(Exception ex)
            {
                //NLOG
                Console.WriteLine("get cadeteid error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<MostrarCadeteViewModel>? GetAll()
        {
            try {
                var listaCadetes = new List<MostrarCadeteViewModel>();
                var connection = GetConnection();
                // var queryString = "SELECT cadete.*, count(id_pedido) AS pedidos FROM cadete LEFT JOIN pedido USING(id_cadete) WHERE estado NOT IN ('Entregado', 'Anulado') OR estado IS NULL GROUP BY id_cadete;";
                var queryString = "SELECT * FROM cadete;";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    MostrarCadeteViewModel nuevo;
                    while (reader.Read())
                    {
                        nuevo = new MostrarCadeteViewModel();
                        nuevo.Id = Convert.ToInt32(reader["id_cadete"]);
                        nuevo.Nombre = reader["cadete"].ToString();
                        nuevo.Telefono = reader["telefono"].ToString();
                        nuevo.Direccion = reader["direccion"].ToString();
                        // nuevo.NroPendientes = Convert.ToInt32(reader["pendientes"]);
                        listaCadetes.Add(nuevo);
                    }
                }
                connection.Close();
                return listaCadetes;
            }
            catch(Exception ex)
            {
                //Nlog
                Console.WriteLine("mostrar cadetes error");
                Console.WriteLine(ex);
                return null;
            }
            
        }

        public void Save(Cadete cadete)
        {
            try {
                string? nombre = cadete.Nombre;
                string? direccion = cadete.Direccion;
                string? telefono = cadete.Telefono;
                var connection = GetConnection();
                var queryString = $"INSERT INTO cadete(cadete, direccion, telefono, id_cadeteria) VALUES ('{nombre}', '{direccion}', '{telefono}', 1);";
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

        public void Update(Cadete cadete)
        {
            try {
                string? nombre = cadete.Nombre;
                string? direccion = cadete.Direccion;
                string? telefono = cadete.Telefono;
                var connection = GetConnection();
                var queryString = $"UPDATE cadete SET cadete = '{nombre}', direccion = '{direccion}', telefono = '{telefono}' WHERE id_cadete = {cadete.Id};";
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







    }
}