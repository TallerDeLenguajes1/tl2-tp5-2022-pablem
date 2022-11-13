using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class AsignarCadeteViewModel 
    {
        public int IdPedido { get; set; }
        
        [Required][Display(Name="Cadetes Disponibles")]
        public int IdCadete { get; set; }

        public SelectList? Cadetes { get; set; }
    }
}