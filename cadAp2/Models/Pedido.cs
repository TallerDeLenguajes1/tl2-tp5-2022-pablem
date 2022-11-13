namespace Models
{
    public class Pedido
    {

        public int Id { get ; set ; }
        public string Detalle { get ; set ; }
        public EstadoPedido Estado { get ; set ; }
        // public Cliente Cliente { get ; set ; }

        public string DetalleCorto()
        {
            return (Detalle.Length < 16) ? Detalle : Detalle.Remove(12)+"...";
        }

        // public string mostrar()
        // {
        //     return $"+ Pedido: {id} - Cliente: {cliente.Nombre} - Estado: {estado}";
        // }


    }
}