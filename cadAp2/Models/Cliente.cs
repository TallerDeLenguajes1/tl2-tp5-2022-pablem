namespace cadAp2.Models
{
    public class Cliente
    {
        static int nroPersonas = 0;

        int id;
        string nombre;
        string direccion;
        string referenciaDireccion;
        string telefono;

        //Constructor
        public Cliente() {
            this.id = ++nroPersonas;
        }
        public Cliente(string nombre, string direccion, string referencia, string telefono)
        {
            this.id = ++nroPersonas;
            this.nombre = nombre;
            this.direccion = direccion;
            this.referenciaDireccion = referencia;
            this.telefono = telefono;
        }
        //Getters & Setters
        // public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }

    
}
