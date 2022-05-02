using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class EquipoTrabajo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Trabajador> Trabajador { get; set; }
        public ICollection<Turno> Turnos { get; set; }

        public EquipoTrabajo() { }

        public EquipoTrabajo(string Nombre)
        {
            this.Nombre = Nombre;
        }
    }
}
