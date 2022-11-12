using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class AltaPedidoViewModel
    {
        public int IdCliente { get; set; }

        [Required][StringLength(100)][Display(Name="Información del pedido")]
        public string? Detalles { get; set; }

        public SelectList? ListaClientes { get; set; }

        [Required][StringLength(50)][Display(Name="Nombre del cliente")]
        public string? Nombre { get; set; }

        [Required][StringLength(100)][Display(Name="Dirección de destino")]
        public string? Direccion  { get; set; }
//text area??
        [StringLength(100)][Display(Name="Detalles sobre la dirección")]
        public string? ReferenciaDireccion  { get; set; }
        
        [Required][Phone][StringLength(60, MinimumLength = 10)]
        [Display(Name="Teléfono")]
        public string? Telefono  { get; set; }
    }
}