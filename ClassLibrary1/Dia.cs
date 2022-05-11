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
        public bool Disfrutado { get; set; }
        public virtual Calendario CalendarioPertenece { get; set; }

        public Dia()
        {

        }
        public Dia(string Motivo, DateTime Fecha)
        {
            this.Motivo = Motivo;
            this.Fecha = Fecha;
        }
        public static List<DateTime> GetFechas(List<Dia> lista)
        {
            List<DateTime> fechas = new List<DateTime>();
            foreach (Dia dia in lista)
            {
                fechas.Add(dia.Fecha.Date);
            }
            return fechas;
        }
    }
}
