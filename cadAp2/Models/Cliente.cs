namespace cadAp2.Models
{
    public class Cliente
    {
        int id;
        string nombre;
        string direccion;
        string referenciaDireccion;
        string telefono;

        //Constructor
        public Cliente(string nombre, string direccion, string referencia, string telefono)
        {
            this.id = 0;
            this.nombre = nombre;
            this.direccion = direccion;
            this.referenciaDireccion = referencia;
            this.telefono = telefono;
        }
        //Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }

    
}
