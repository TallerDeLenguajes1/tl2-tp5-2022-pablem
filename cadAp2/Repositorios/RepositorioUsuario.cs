using System.Data.SQLite;
using CadenasDeconexion;
using Models;

namespace Repositorios
{
    public class RepositorioUsuarioSQlite : IRepositorioUsuario
    {
        private readonly string _cadenaDeConexion;

        public RepositorioUsuarioSQlite(ICadenaDeConexion cadenaDeConexion)
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

        public List<Usuario>? GetAll()
        {
            try
            {
                var listaUsuarios = new List<Usuario>();
                var connection = GetConnection();
                var queryString = "SELECT * FROM usuario;";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    Usuario nuevo;
                    while (reader.Read())
                    {
                        nuevo = new Usuario();
                        nuevo.Id = Convert.ToInt32(reader["id_usuario"]);
                        nuevo.Nombre = reader["nombre"].ToString();
                        nuevo.NikName = reader["usuario"].ToString();
                        nuevo.Password = reader["password"].ToString();
                        Enum.TryParse(reader["rol"].ToString(), out RolUsuario aux);
                        nuevo.Rol = aux;
                        listaUsuarios.Add(nuevo);
                    }
                }
                connection.Close();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                //Nlog
                Console.WriteLine("error obtener todos los usuarios");
                Console.WriteLine(ex);
                return null;
            }
        }








    }
}