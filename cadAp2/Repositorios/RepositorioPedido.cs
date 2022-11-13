
using System.Data.SQLite;
using Models;
using ViewModels;

namespace Repositorios
{
    public class RepositorioPedidoSQLite// : IRepositorioCadete
    {
        private SQLiteConnection GetConnection()
        {
            var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            return connection;
        }

        public Pedido? GetPedido(int? id)
        {
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
                        nuevo.Detalle = reader["detalle"].ToString()!;
                        Enum.TryParse(reader["estado"].ToString(), out EstadoPedido estadoAux);
                        nuevo.Estado = estadoAux;
                    }
                }
                connection.Close();
                return nuevo;
            }
            catch(Exception ex)
            {
                //NLOG
                Console.WriteLine("get pedoido por id error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public ModificarPedidoViewModel? GetPedidoYCliente(int? id)
        {
            try {
                var connection = GetConnection();
                var queryString = $"SELECT * FROM pedido INNER JOIN cliente USING(id_cliente) WHERE id_pedido = {id};";
                var comando = new SQLiteCommand(queryString, connection);

                var nuevo = new ModificarPedidoViewModel();
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nuevo = new ModificarPedidoViewModel();
                        nuevo.IdPedido = Convert.ToInt32(reader["id_pedido"]);
                        nuevo.IdCliente = Convert.ToInt32(reader["id_cliente"]);
                        nuevo.Detalles = reader["detalle"].ToString()!;
                        Enum.TryParse(reader["estado"].ToString(), out EstadoPedido estadoAux);
                        nuevo.Estado = estadoAux;
                        nuevo.Nombre = reader["cliente"].ToString();
                        nuevo.Direccion = reader["direccion"].ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("referencia_direccion")))
                            nuevo.ReferenciaDireccion = reader["referencia_direccion"].ToString();
                        nuevo.Telefono = reader["telefono"].ToString();
                    }
                }
                connection.Close();
                return nuevo;
            }
            catch(Exception ex)
            {
                //NLOG
                Console.WriteLine("error get peiddo con cliente");
                Console.WriteLine(ex);
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
                //nLOg
                Console.WriteLine(ex);
            }
        }

        public void Update(Pedido pedido)
        {
            try {
                int id = pedido.Id;
                string? detalles = pedido.Detalle;
                string? estado = pedido.Estado.ToString();
                var connection = GetConnection();
                var queryString = $"UPDATE pedido SET detalle = '{detalles}', estado = '{estado}' WHERE id_pedido = {id};";
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
                var queryString = $"DELETE FROM pedido WHERE id_pedido = {id};";
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