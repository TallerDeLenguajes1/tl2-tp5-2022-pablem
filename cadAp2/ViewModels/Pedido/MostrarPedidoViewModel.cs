using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class MostrarPedidoViewModel 
    {
        public int Id { get; set; }
        public string? Detalles { get; set; }
        [Display(Name="Cliente")]
        public string? NombreCliente { get; set; }
        [Display(Name="Dirección")]
        public string? Direccion { get; set; }
        public cadAp2.Models.EstadoPedido Estado { get; set; }
        
        public string DetalleCorto()
        {
            return (Detalles.Length < 16) ? Detalles : Detalles.Remove(12)+"...";
        }
    }
}