using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class TrabajadorEnTurno
    {
        [Key]
        public int Id { get; set; }
        public Trabajador trabajador { get; set; }
        public Fichajes fichaje { get; set; }

        public TrabajadorEnTurno()
        {

        }
        public TrabajadorEnTurno(Trabajador trab, Fichajes fich)
        {
            this.trabajador = trab;
            this.fichaje = fich;
        }
    }
}
