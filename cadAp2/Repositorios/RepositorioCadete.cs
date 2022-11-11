
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
            var cadenaDeConexion = @"Data Source=db\cadeteria22.sql;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            var queryString = "SELECT * FROM cadete;";
            var comando = new SQLiteCommand(queryString, connection);
            
            using(var reader = comando.ExecuteReader()) 
            {
                while(reader.Read())
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
            return listaCadetes;
        }
        
    }
}