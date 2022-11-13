using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ModificarClienteViewModel
    {
        public int Id { get; set; }

        [Required][StringLength(50)]
        public string? Nombre { get; set;}

        [Required][Phone][StringLength(13, MinimumLength = 10)]
        [Display(Name="Teléfono")]
        public string? Telefono { get; set;}

        [Required][Display(Name="Dirección")]
        public string? Direccion { get; set;}

        [Display(Name="Referencia (Opcional)")]
        public string? ReferenciaDireccion { get; set;}
    }
}