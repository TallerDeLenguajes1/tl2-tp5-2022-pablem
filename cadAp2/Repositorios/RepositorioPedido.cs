
using System.Data.SQLite;
using Models;
using ViewModels;

namespace Repositorios
{
    public interface IRepositorioPedido
    {
        int? ProxId();
        Pedido? GetById(int? id);
        List<MostrarPedidoViewModel>? GetAll();
        ModificarPedidoViewModel? GetPedidoYCliente(int? id);
        void Save(AltaPedidoViewModel pedido);
        void Update(Pedido pedido);
        void Delete(int id);
        List<MostrarPedidoViewModel>? PedidosPorCadete(int id);
        List<MostrarPedidoViewModel>? PedidosPorCliente(int id);
        int ObtenerCadeteId(int idPedido);
        void AsignarCadete(AsignarCadeteViewModel asignar);
    }

    public class RepositorioPedidoSQLite : IRepositorioPedido
    {
        private SQLiteConnection GetConnection()
        {
            var cadenaDeConexion = @"Data Source=cadeteria.db;Version=3;";
            var connection = new SQLiteConnection(cadenaDeConexion);
            connection.Open();
            return connection;
        }

        public int? ProxId()
        {
            try
            {
                int idNuevo;
                var connection = GetConnection();
                var queryString = $"SELECT max(id_pedido)+1 FROM pedido;";
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
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("get pedoido por id error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public ModificarPedidoViewModel? GetPedidoYCliente(int? id)
        {
            try
            {
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
                        nuevo.Detalle = reader["detalle"].ToString()!;
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
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("error get peiddo con cliente");
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<MostrarPedidoViewModel>? GetAll()
        {
            try
            {
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
            catch (Exception ex)
            {
                //NLOG
                Console.WriteLine("recuperando todos los pedidos error");
                Console.WriteLine(ex);
                return null;
            }
        }

        public void Save(AltaPedidoViewModel pedido)
        {
            try
            {
                string? detalle = pedido.Detalle;
                string? estado = EstadoPedido.Pendiente.ToString();
                int id = pedido.IdCliente;
                var connection = GetConnection();
                var queryString = $"INSERT INTO pedido(detalle, estado, id_cliente) VALUES ('{detalle}', '{estado}', {id});";
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

        public void AsignarCadete(AsignarCadeteViewModel asignar)
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