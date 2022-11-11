
using System.Data.SQLite;
using cadAp2.Models;

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

        public List<Cadete> GetAll()
        {
            //var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var listaCadetes = new List<Cadete>();
            try {
                var connection = GetConnection();
                var queryString = "SELECT * FROM cadete;";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    Cadete nuevo;
                    while (reader.Read())
                    {
                        nuevo = new Cadete();
                        nuevo.Id = Convert.ToInt32(reader["id_cadete"]);
                        nuevo.Nombre = reader["cadete"].ToString();
                        nuevo.Telefono = reader["telefono"].ToString();
                        nuevo.Direccion = reader["direccion"].ToString();
                        listaCadetes.Add(nuevo);
                    }
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                //N
            }
            return listaCadetes;
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
            }
        }

        private SQLiteConnection GetConnection()
        {
            var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            return connection;
        }

    }
}