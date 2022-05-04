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
        public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
        string trabajadores;
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        string turnos;
        public ICollection<Tareas> Tareas { get; set; } = new List<Tareas>();
        string tareas;
        public ICollection<Zonas> Zonas { get; set; } = new List<Zonas>();
        string zonas;

        public EquipoTrabajo() { }

        public EquipoTrabajo(string Nombre)
        {
            this.Nombre = Nombre;
        }
    }
}
