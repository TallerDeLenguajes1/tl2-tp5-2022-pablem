namespace Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? NikName { get; set; }
        public string? Nombre { get; set; }
        public string? Password { get; set; }
        public RolUsuario Rol { get; set; }
    }
}