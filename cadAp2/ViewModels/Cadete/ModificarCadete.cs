using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ModificarCadeteViewModel
    {
        public int Id { get; set; }

        [Required][StringLength(50)][Display(Name="Nombre del Cadete")]
        public string? Nombre { get; set;}

        [Required][Phone][StringLength(13, MinimumLength = 10)]
        public string? Telefono { get; set;}

        [Required]
        public string? Direccion { get; set;}
    }
}