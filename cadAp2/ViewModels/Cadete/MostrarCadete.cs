using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class MostrarCadeteViewModel 
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        [Display(Name="Pedidos Pendientes")]
        public int NroPendientes { get; set; }
    }
}