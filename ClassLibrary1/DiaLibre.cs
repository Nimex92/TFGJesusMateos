using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class DiaLibre
    {
        [Key]
        public int Id { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsFestivo { get; set; }
        public ICollection<Calendario> CalendarioPertenece { get; set; }

        public DiaLibre()
        {

        }
        public DiaLibre(string Motivo, DateTime Fecha,bool EsFestivo)
        {
            this.Motivo = Motivo;
            this.Fecha = Fecha;
            this.EsFestivo = EsFestivo;
        }
    }
}
