
using System.Data.SQLite;
using cadAp2.Models;

namespace Repositorios
{
    public class RepositorioCadete
    {
        public static List<Cadete> GetCadetes()
        {
            Cadete nuevo;
            var listaCadetes = new List<Cadete>();
            var cadenaDeConexion = @"Data Source=db\Cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            var queryString = "SELECT * FROM Cadete;";
            var comando = new SQLiteCommand(queryString, connection);
            
            using(var reader = comando.ExecuteReader()) 
            {
                while(reader.Read())
                {
                    nuevo = new Cadete();
                    nuevo.Id = Convert.ToInt32(reader["Id"]);
                    nuevo.Nombre = reader["Nombre"].ToString();
                    nuevo.Telefono = reader["Telefono"].ToString();
                    nuevo.Direccion = reader["Direccion"].ToString();
                    listaCadetes.Add(nuevo);
                }
            }

            connection.Close();
            return listaCadetes;
        }
        
    }
}