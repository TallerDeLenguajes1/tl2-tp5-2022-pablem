namespace Models
{
    public class Cadete
    {
        public Cadete() {
            ListaPedidos = new List<Pedido>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public List<Pedido>? ListaPedidos { get; set; }

        //Métodos
        public int NroPendientes()
        {
            return ListaPedidos.Where(ped => ped.Estado == EstadoPedido.Viajando).Count();
        }

        public int calcularPago() 
        { 
            int pago = 0;
            foreach (var pedido in this.pedidosEntregados()) {
                pago += 300;
            }
            return pago;
        }

        public List<Pedido> pedidosEntregados() 
        {

            /*-------- Consultas LINQ en C# ----------*/
            // var query =
            //     from cad in listaPedidos
            //     where cad.Estado==EstadoPedido.Entregado
            //     select cad;
            /*----------------------------------------*/
            // return query.ToList();
            //x => x.Estado == EstadoPedido.Entregado
            return ListaPedidos;
        }
    }
}
