using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class TareaComenzada
    {
        public int id { get; set; } 
        public Tareas tarea { get; set; }   
        public Trabajador trabajador { get; set; }   
        public DateTime InicioTarea { get; set; }

        public TareaComenzada()
        {

        }
        public TareaComenzada(Tareas t,Trabajador tr,DateTime Init)
        {
            this.tarea = t;
            this.trabajador = tr;
            this.InicioTarea = Init;
        }
    }
}
