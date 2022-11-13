using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class AltaPedidoViewModel
    {
        [Required][Display(Name="Seleccione Cliente o Registre uno Nuevo")]
        public int IdCliente { get; set; }

        [Required][StringLength(100)][Display(Name="Información del pedido")]
        public string? Detalle { get; set; }
//text area??
        public SelectList? ListaClientes { get; set; }
    }
}