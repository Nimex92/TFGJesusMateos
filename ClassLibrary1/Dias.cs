using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Dias
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public bool FestivoNoFestivo { get; set; }
        public Trabajador DuenoDeCalendario { get; set; }
        public Dias(DateTime Fecha, string Motivo, bool FestivoNoFestivo)
        {
            this.Fecha = Fecha;
            this.Motivo = Motivo;
            this.FestivoNoFestivo = FestivoNoFestivo;

        }
    }
    
}
