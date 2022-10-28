namespace cadAp2.Models 
{
    public class Cadeteria
    {
        string nombre;
        string telefono;
        List<Cadete> listaCadetes;

        //Constructor
        public Cadeteria(string nombre, string telefono, List<Cadete> listaCadetes)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.listaCadetes = listaCadetes;
        }

        //Getters & Setters
        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }

        //Métodos
        public int calcularPago() { ///control por fechas?
            int pago = 0;
            foreach (var cadete in listaCadetes)
            {
                pago += cadete.calcularPago(); 
            }
            return pago;
        }

        public float calcularPromedio() 
        {
            int nroPeiddos = 0;
            foreach (var cadete in listaCadetes)
            {
                nroPeiddos += cadete.pedidosEntregados().Count;
                Console.WriteLine(cadete.pedidosEntregados().Count);
            }    
            return nroPeiddos/(float)(listaCadetes.Count);
        }

        public void mostrarPedidos() {
            foreach (var cadete in listaCadetes)
            {
                Console.WriteLine($"\n-Cadete Cód. {cadete.Id} - Nro. de Pedidos: {cadete.pedidosEntregados().Count} - Ganancia: {cadete.calcularPago()}");
            }
        }
    }
}