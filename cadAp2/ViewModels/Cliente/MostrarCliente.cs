using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class MostrarClienteViewModel 
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        [Display(Name="Teléfono")]
        public string? Telefono { get; set; }
        [Display(Name="Dirección")]
        public string? Direccion { get; set; }
        [Display(Name="Referencias")]
        public string? ReferenciaDireccion { get; set; }
    }
}