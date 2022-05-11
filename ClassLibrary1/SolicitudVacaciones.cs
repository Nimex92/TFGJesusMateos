using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class SolicitudVacaciones
    {
        [Key]
        public int Id { get; set; }
        public string Trabajador { get; set; }
        public DateTime Fecha { get; set; }
        public bool SeAcepta { get; set; }

        public SolicitudVacaciones()
        {

        }
        public SolicitudVacaciones(string trab, DateTime fecha, bool SeAcepta)
        {
            this.Trabajador = trab;
            this.SeAcepta = SeAcepta;
            this.Fecha = fecha;
        }
    }
}
