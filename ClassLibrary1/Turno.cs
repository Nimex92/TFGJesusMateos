using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Turno
    {
        [Key]
        public string Nombre { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public bool EsLunes { get; set; }
        public bool EsMartes { get; set; }
        public bool EsMiercoles { get; set; }
        public bool EsJueves { get; set; }
        public bool EsViernes { get; set; }
        public bool EsSabado { get; set; }
        public bool EsDomingo { get; set; }
        public DateTime ValidoDesde { get; set; }
        public DateTime ValidoHasta { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<EquipoTrabajo> EquiposDeTrabajo { get; set; }
        public Turno()
        {

        }
        public Turno(string Nombre,DateTime HoraEntrada,DateTime HoraSalaida,bool L,bool M,bool X,bool J,bool V,bool S,bool D)
        {
            this.Nombre = Nombre;
            this.HoraEntrada = HoraEntrada;
            this.HoraSalida = HoraSalaida;
            this.EsLunes = L;
            this.EsMartes = M;
            this.EsMiercoles = X;
            this.EsJueves = J;
            this.EsViernes = V;
            this.EsSabado = S;
            this.EsDomingo = D;
        }
    }
}
