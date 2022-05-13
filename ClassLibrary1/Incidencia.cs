using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Incidencia
    {
        public int Id { get; set; }
        public Trabajador Trabajador { get; set; }
        public string MotivoIncidencia { get; set; }
        public DateTime FechaIncidencia { get; set; }
        public bool Justificada { get; set; }

        public Incidencia() { }

        public Incidencia(Trabajador trab, string motivo, DateTime fecha,bool justificada)
        {
            this.Trabajador=trab;
            this.MotivoIncidencia = motivo;
            this.FechaIncidencia = fecha;
            this.Justificada=justificada;
        }
    }
}
