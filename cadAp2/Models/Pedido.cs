namespace Models
{
    public class Pedido
    {

        public int Id { get ; set ; }
        public string Detalle { get ; set ; }
        public EstadoPedido Estado { get ; set ; }
        public Cliente Cliente { get ; set ; }

        public Pedido() {
            Cliente = new Cliente();
        }

        public string DetalleCorto()
        {
            return (Detalle.Length < 16) ? Detalle : Detalle.Remove(12)+"...";
        }


    }
}