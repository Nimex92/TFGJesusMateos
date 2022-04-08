using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Tareas
    {
        [Key]
        public int IdTarea { get; set; }
        public string NombreTarea { get; set; }
        public string Descripcion { get; set; }
        public int TiempoEstimado { get; set; }

        public Tareas()
        {

        }
        public Tareas(string NombreTarea,string Descripcion,int TiempoEstimado)
        {
            this.NombreTarea = NombreTarea;
            this.Descripcion = Descripcion;
            this.TiempoEstimado = TiempoEstimado;
        }
    }
}
