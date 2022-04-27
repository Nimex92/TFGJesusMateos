using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Calendario
    {
        public int Id { get; set; }
        public Trabajador Trabajador { get; set; }
        public ICollection<DiaLibre> DiasDelCalendario { get; set; }

        public Calendario() { }
        public Calendario(Trabajador trab)
        {
            Trabajador = trab;
        }
    }
}
