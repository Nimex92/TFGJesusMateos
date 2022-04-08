using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class TareaRealizada
    {
        public int id { get; set; } 
        public Tareas tarea { get; set; }   
        public Trabajador trabajador { get; set; }  
        public Grupo_Trabajo grupo { get; set; } 
        public DateTime InicioTarea { get; set; }
        public DateTime FinTarea { get; set; }
        public double HorasUsadas { get; set; }

        public TareaRealizada()
        {

        }
        public TareaRealizada(Tareas t,Trabajador tr,Grupo_Trabajo gr,DateTime Init,DateTime End,double Usadas)
        {
            this.tarea = t;
            this.trabajador = tr;
            this.grupo = gr;
            this.InicioTarea = Init;
            this.FinTarea = End;
            this.HorasUsadas = Usadas;
        }
    }
}
