
using System.Data.SQLite;
using Models;
using ViewModels;

namespace Repositorios
{
    // public interface IRepositorioCadete
    // {
    //     List<Cadete> GetCadetes();
    // }

    public class RepositorioPedidoSQLite// : IRepositorioCadete
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

        public Pedido? GetPedido(int? id)
        {
            //var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            try {
                var connection = GetConnection();
                var queryString = $"SELECT * FROM pedido WHERE id_pedido = {id};";
                var comando = new SQLiteCommand(queryString, connection);

                var nuevo = new Pedido();
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nuevo = new Pedido();
                        nuevo.Id = Convert.ToInt32(reader["id_pedido"]);
                        nuevo.Detalles = reader["detalle"].ToString();
                        nuevo.Estado = (EstadoPedido)reader["estado"];
                    }
                }
                connection.Close();
                return nuevo;
            }
            catch(Exception ex)
            {
                //NLOG
                Console.WriteLine("get pedoido por id error");
                return null;
            }
        }

        public List<MostrarPedidoViewModel>? GetAll()
        {
            try {
                var listaPedidos = new List<MostrarPedidoViewModel>();
                var connection = GetConnection();
                // var queryString = "SELECT id_pedido, detalle, cliente, (SUBSTRING(detalle, 1, 15) || '...') AS detalleCorto, estado, cadete FROM pedido INNER JOIN cliente USING(id_cliente) LEFT JOIN cadete USING(id_cadete);";
                var queryString = "SELECT id_pedido, (SUBSTRING(detalle, 1, 15) || '...') AS detalleCorto, estado, cliente, direccion FROM pedido INNER JOIN cliente USING(id_cliente);";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    MostrarPedidoViewModel nuevo;
                    while (reader.Read())
                    {
                        nuevo = new MostrarPedidoViewModel();
                        nuevo.Id = Convert.ToInt32(reader["id_pedido"]);
                        nuevo.DetalleCorto = reader["detalleCorto"].ToString();
                        Enum.TryParse(reader["estado"].ToString(), out EstadoPedido estadoAux);
                        nuevo.Estado = estadoAux;
                        nuevo.NombreCliente = reader["cliente"].ToString();
                        nuevo.Direccion = reader["direccion"].ToString();
                        // if (!reader.IsDBNull(reader.GetOrdinal("cadete")))
                        //     nuevo.NombreCadete = reader["cadete"].ToString();
                        listaPedidos.Add(nuevo);
                    }
                }
                connection.Close();
                return listaPedidos;
            }
            catch(Exception ex)
            {
                //NLOG
                Console.WriteLine("recuperando todos los pedidos error");
                Console.WriteLine(ex);
                return null;
            }
            
        }

        public void Save(AltaPedidoViewModel pedido)
        {
            try {
                string? detalle = pedido.Detalles;
                string? estado = EstadoPedido.Pendiente.ToString();
                int id = pedido.IdCliente;
                var connection = GetConnection();
                var queryString = $"INSERT INTO pedido(detalle, estado, id_cliente) VALUES ('{detalle}', '{estado}', {id});";
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
                //throw;
            }
            
        }







    }
}