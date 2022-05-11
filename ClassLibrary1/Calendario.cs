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
        public virtual Trabajador Trabajador { get; set; }
        public ICollection<Dia> DiasDelCalendario { get; set; } = new List<Dia>();

        public Calendario() { }
        public Calendario(Trabajador trab)
        {
            Trabajador = trab;
        }
    }
}
