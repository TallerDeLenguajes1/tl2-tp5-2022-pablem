using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ModificarPedidoViewModel
    {
        public int Id { get; set; }

        [Required][StringLength(100)][Display(Name="Detalles del pedido")]
        public string? Detalles { get; set; }

        [Required]
        public cadAp2.Models.EstadoPedido Estado { get; set; }

        [Required][StringLength(50)][Display(Name="Nombre del cliente")]
        public string? Nombre { get; set; }

        [Required][StringLength(100)][Display(Name="Dirección de destino")]
        public string? Direccion  { get; set; }
//text area??
        [StringLength(100)][Display(Name="Dirección de destino")]
        public string? ReferenciaDireccion  { get; set; }
        
        [Required][Phone][StringLength(60, MinimumLength = 10)]
        [Display(Name="Teléfono")]
        public string? Telefono  { get; set; }
    }
}