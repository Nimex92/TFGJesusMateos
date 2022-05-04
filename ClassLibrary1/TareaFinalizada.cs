using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class TareaFinalizada
    {

        public int id { get; set; }
        public Tareas tarea { get; set; }
        public Trabajador trabajador { get; set; }
        public DateTime inicioTarea { get; set; }
        public DateTime FinTarea { get; set; }
        public double HorasUsadas { get; set; }
        public bool EnHora { get; set; }

        public TareaFinalizada()
        {

        }
        public TareaFinalizada(Tareas t, Trabajador tr, DateTime init, DateTime Fin, double HorasUsadas,bool EnHora)
        {
            this.tarea = t;
            this.trabajador = tr;
            this.inicioTarea = init;
            this.FinTarea = Fin;
            this.HorasUsadas = HorasUsadas;
            this.EnHora = EnHora;
        }
    }
}
