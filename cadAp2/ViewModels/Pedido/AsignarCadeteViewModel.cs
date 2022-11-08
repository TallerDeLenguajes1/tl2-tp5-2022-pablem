using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class AsignarCadeteViewModel 
    {
        // public int Id { get; set; }
        // public string? Nombre { get; set; }
        // [Display(Name="Pedidos Pendientes")]
        // public int NroPendientes { get; set; }
        public SelectList? Lista { get; set; }
    }
}