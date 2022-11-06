using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class MostrarPedidoViewModel 
    {
        public int Id { get; set; }
        public string? Detalles { get; set; }
        public string? NombreCliente { get; set; }
        public string? Direccion { get; set; }
        public cadAp2.Models.EstadoPedido Estado { get; set; }

        public string DetalleCorto()
        {
            return (Detalles.Length < 11) ? Detalles : Detalles.Remove(7)+"...";
        }
    }
}