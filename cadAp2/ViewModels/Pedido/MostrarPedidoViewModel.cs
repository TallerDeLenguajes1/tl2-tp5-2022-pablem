using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class MostrarPedidoViewModel 
    {
        public int Id { get; set; }
        [Display(Name="Detalles")]
        public string? DetalleCorto { get; set; }
        [Display(Name="Cliente")]
        public string? NombreCliente { get; set; }
        [Display(Name="Dirección")]
        public string? Direccion { get; set; }
        public cadAp2.Models.EstadoPedido Estado { get; set; }
        [Display(Name="Cadete")]
        public string? NombreCadete { get; set; } 
    }
}