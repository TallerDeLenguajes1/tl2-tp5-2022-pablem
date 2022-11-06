using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AltaCadeteViewModel
    {
        [Required][StringLength(50)][Display(Name="Nombre del Cadete")]
        public string? Nombre { get; set;}

        [Required][Phone][StringLength(60, MinimumLength = 10)]
        [Display(Name="Teléfono")]
        public string? Telefono { get; set;}

        [Required][Display(Name="Dirección particular")]
        public string? Direccion { get; set;}
    }
}