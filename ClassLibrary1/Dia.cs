using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Dia
    {
        [Key]
        public int Id { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsFestivo { get; set; }
        public Calendario CalendarioPertenece { get; set; }

        public Dia()
        {

        }
        public Dia(string Motivo, DateTime Fecha,bool EsFestivo)
        {
            this.Motivo = Motivo;
            this.Fecha = Fecha;
            this.EsFestivo = EsFestivo;
        }
    }
}
