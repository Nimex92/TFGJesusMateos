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
        public EquipoTrabajo EquipoTrabajo { get; set; }
        public DateTime FechaFichaje { get; set; }
        public string Entrada_Salida { get; set; }

        public Fichajes(int trab, int grup_trab, DateTime fechaFichaje,string entr_sali)
        {
            this.Trabajador.numero_tarjeta = trab;
            this.EquipoTrabajo.Id = grup_trab;
            this.FechaFichaje = fechaFichaje;
            this.Entrada_Salida = entr_sali;
        }
        public Fichajes(Trabajador trab, EquipoTrabajo grup_trab, DateTime fechaFichaje, string entrada_Salida)
        {
            this.Trabajador = trab;
            this.EquipoTrabajo = grup_trab;
            this.FechaFichaje = fechaFichaje;
            this.Entrada_Salida = entrada_Salida;
        }
    }
}