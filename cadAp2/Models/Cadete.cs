namespace cadAp2.Models
{
    public class Cadete
    {
        // int id;
        // string nombre;
        // string direccion;
        // string telefono;
        // List<Pedido> listaPedidos;

        //Constructor
        // public Cadete(string nombre, string direccion, string telefono)
        // {
        //     this.id = 0;
        //     this.nombre = nombre;
        //     this.direccion = direccion;
        //     this.telefono = telefono;
        //     listaPedidos = new List<Pedido>();
        // }
        //Getters & Setters
        // public int Id { get => id; set => id = value; }
        // public string? Nombre { get => nombre; set => nombre = value; }
        // public string? Direccion { get => direccion; set => direccion = value; }
        // public string? Telefono { get => telefono; set => telefono = value; }
        // public List<Pedido> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public List<Pedido> ListaPedidos { get; set; }

        //Métodos

        public void agregarPedido(Pedido pedido) 
        {
            ListaPedidos.Add(pedido);
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
