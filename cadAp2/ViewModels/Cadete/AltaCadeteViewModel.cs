using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AltaCadeteViewModel
    {
        [Required][StringLength(50)][Display(Name="Nombre del Cadete")]
        public string Nombre { get; set;}

        [Required][Phone][StringLength(60, MinimumLength = 10)]
        public string Telefono { get; set;}

        [Required]
        public string Direccion { get; set;}
    }
}