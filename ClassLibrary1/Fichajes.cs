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
        public Grupo_Trabajo Grupo_Trabajo { get; set; }
        public DateTime FechaFichaje { get; set; }

        public string Entrada_Salida { get; set; }


        public Fichajes(int trab, int grup_trab, DateTime fechaFichaje,string entr_sali)
        {
            this.Trabajador.numero_tarjeta = trab;
            this.Grupo_Trabajo.IdGrupo = grup_trab;
            this.FechaFichaje = fechaFichaje;
            this.Entrada_Salida = entr_sali;
        }
        public Fichajes(Trabajador trab, Grupo_Trabajo grup_trab, DateTime fechaFichaje, string entrada_Salida)
        {
            this.Trabajador = trab;
            this.Grupo_Trabajo = grup_trab;
            this.FechaFichaje = fechaFichaje;
            this.Entrada_Salida = entrada_Salida;
        }
    }
}