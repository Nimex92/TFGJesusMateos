using Bibliotec;

namespace ClassLibrary1
{
    public class Fichajes
    {
        public Fichajes()
        {

        }

        public int Id { get; set; }
        public Trabajador Trabajador { get; set; }
        public DateTime FechaFichaje { get; set; }
        public string Entrada_Salida { get; set; }

        public Fichajes(Trabajador trab, DateTime FechaFichaje, string Entrada_Salida)
        {
            this.Trabajador = trab;
            this.FechaFichaje = FechaFichaje;
            this.Entrada_Salida = Entrada_Salida;
        }
        public static List<DateTime> GetFechas(List<Fichajes> lista)
        {
            List<DateTime> fechas = new List<DateTime>();
            foreach(Fichajes fichajes in lista)
            {
                fechas.Add(fichajes.FechaFichaje.Date);
            }
            return fechas;
        }
    }
}