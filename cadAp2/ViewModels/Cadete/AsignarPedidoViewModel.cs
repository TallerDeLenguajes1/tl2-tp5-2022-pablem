using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class AsignarPedidoViewModel 
    {
        public int IdCadete { get; set; }
        // public string? Nombre { get; set; }
        
        [Required][Display(Name="Pedidos Pendientes")]
        public int IdPedido { get; set; }

        public SelectList? Pedidos { get; set; }
    }
}