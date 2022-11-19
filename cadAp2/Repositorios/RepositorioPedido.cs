
using System.Data.SQLite;
using CadenasDeconexion;
using Models;
using ViewModels;/////NO VAA corregir!

namespace Repositorios
{
    public class RepositorioPedidoSQLite : IRepositorioPedido
    {
        private readonly string _cadenaDeConexion;

        public RepositorioPedidoSQLite(ICadenaDeConexion cadenaDeConexion)
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

        private static Pedido CrearPedido(SQLiteDataReader reader)
        {
            Pedido nuevo = new Pedido();
            nuevo.Id = Convert.ToInt32(reader["id_pedido"]);
            nuevo.Detalle = reader["detalle"].ToString()!;
            Enum.TryParse(reader["estado"].ToString(), out EstadoPedido estadoAux);
            nuevo.Estado = estadoAux;
            return nuevo;
        }
        /*DUDA: no puedo importar desde el metodo publico desde IRepositorioCliente*/
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

        public int? GetLastId()
        {
            try
            {
                int idNuevo;
                var connection = GetConnection();
                var queryString = $"SELECT max(id_pedido) FROM pedido;";
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

        public Pedido? GetById(int? id)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"SELECT * FROM pedido INNER JOIN cliente USING(id_cliente) WHERE id_pedido = {id};";
                var comando = new SQLiteCommand(queryString, connection);
                //Pedido nuevo;
                Pedido? nuevo = null;
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nuevo = CrearPedido(reader);
                        nuevo.Cliente = CrearCliente(reader);
                    }
                }
                connection.Close();
                return nuevo;
            }
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("get pedoido por id error");
                Console.WriteLine(ex);
                return null;
            }
        }

        /*Optimizar consultas? en vez de hacer una consulta por cada pedido para recuperar el nombre
        del cadete, podría hacer una sola consulta y guardar el nombre en el objeto de una 
        nueva clase "PedidoConCliente?" 
        var queryString = "SELECT pedido.*, cliente.*, cadete FROM pedido INNER JOIN cliente LEFT JOIN cadete;";*/

        public List<Pedido>? GetAll()
        {
            try
            {
                var listaPedidos = new List<Pedido>();
                var connection = GetConnection();
                var queryString = "SELECT * FROM pedido INNER JOIN cliente USING(id_cliente);";
                var comando = new SQLiteCommand(queryString, connection);

                using (var reader = comando.ExecuteReader())
                {
                    Pedido nuevo;
                    while (reader.Read())
                    {
                        nuevo = CrearPedido(reader);
                        nuevo.Cliente = CrearCliente(reader);
                        listaPedidos.Add(nuevo);
                    }
                }
                connection.Close();
                return listaPedidos;
            }
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("recuperando todos los pedidos error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public void Save(Pedido pedido, int idCliente)
        {
            try
            {
                string? detalle = pedido.Detalle;
                string? estado = EstadoPedido.Pendiente.ToString();
                var connection = GetConnection();
                var queryString = $"INSERT INTO pedido(detalle, estado, id_cliente) VALUES ('{detalle}', '{estado}', {idCliente});";
                var comando = new SQLiteCommand(queryString, connection);
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //nLOg
                Console.WriteLine(ex);
            }
        }

        public void Update(Pedido pedido)
        {
            try
            {
                int id = pedido.Id;
                string? detalles = pedido.Detalle;
                string? estado = pedido.Estado.ToString();
                var connection = GetConnection();
                var queryString = $"UPDATE pedido SET detalle = '{detalles}', estado = '{estado}' WHERE id_pedido = {id};";
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

        public void AsignarCadeteAPedido(int idCadete, int idPedido)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"UPDATE pedido SET id_cadete = {idCadete}, estado = 'Viajando' WHERE id_pedido = {idPedido};";
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

        public void AsignarClienteAPedido(int idCliente, int idPedido)
        {
            try
            {
                var connection = GetConnection();
                var queryString = $"UPDATE pedido SET id_cliente = {idCliente} WHERE id_pedido = {idPedido};";
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

        public int ObtenerCadeteId(int idPedido)
        {
            try
            {
                int idCadete;
                var connection = GetConnection();
                var queryString = $"SELECT id_cadete FROM pedido INNER JOIN cadete USING(id_cadete) WHERE id_pedido = {idPedido};";
                var comando = new SQLiteCommand(queryString, connection);
                idCadete = Convert.ToInt32(comando.ExecuteScalar());
                connection.Close();
                return idCadete;
            }
            catch (Exception ex)
            {
                //Nlog 
                Console.WriteLine(ex);
                return 0;
            }
        }






        /*en proceso de refactorizar*/




        public List<MostrarPedidoViewModel>? PedidosPorCadete(int id)
        {
            try
            {
                var listaPedidos = new List<MostrarPedidoViewModel>();
                var queryString = $"SELECT id_pedido, (SUBSTRING(detalle, 1, 15) || '...') AS detalleCorto, estado, cliente, direccion FROM pedido INNER JOIN cliente USING(id_cliente) WHERE id_cadete = {id};";
                var connection = GetConnection();
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
                        listaPedidos.Add(nuevo);
                    }
                }
                connection.Close();
                return listaPedidos;
            }
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("error recuperando pedidos por cadete");
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<MostrarPedidoViewModel>? PedidosPorCliente(int id)
        {
            try
            {
                var listaPedidos = new List<MostrarPedidoViewModel>();
                var queryString = $"SELECT id_pedido, (SUBSTRING(detalle, 1, 15) || '...') AS detalleCorto, estado, cliente, direccion FROM pedido INNER JOIN cliente USING(id_cliente) WHERE id_cliente = {id};";
                var connection = GetConnection();
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
                        listaPedidos.Add(nuevo);
                    }
                }
                connection.Close();
                return listaPedidos;
            }
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("error recuperando pedidos por cadete");
                Console.WriteLine(ex);
                return null;
            }
        }







    }
}