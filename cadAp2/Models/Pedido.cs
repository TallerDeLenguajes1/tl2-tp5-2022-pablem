namespace cadAp2.Models
{
    public class Pedido
    {
        int id;
        string detalles;
        EstadoPedido estado;
        Cliente cliente;

        //Constructor
        public Pedido() {
            this.estado = EstadoPedido.Pendiente;
        }
        public Pedido(string nomb, string dire, string tel, string refe, string detalles)
        {
            this.detalles = detalles;
            this.estado = EstadoPedido.Pendiente;
            this.cliente = new Cliente(nomb,dire,tel,refe);
        }

        //Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Detalles { get => detalles; set => detalles = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }

        public string DetalleCorto()
        {
            return (Detalles.Length < 16) ? Detalles : Detalles.Remove(12)+"...";
        }

        public string mostrar()
        {
            return $"+ Pedido: {id} - Cliente: {cliente.Nombre} - Estado: {estado}";
        }


    }
}