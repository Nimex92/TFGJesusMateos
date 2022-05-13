using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Nomina
    {
        public int Id { get; set; }
        public Empresa Empresa { get; set; }
        public Trabajador Trabajador { get; set; }
        public int HorasNormales { get; set; }
        public int HorasEspeciales { get; set; }
        public int TotalAPercibir { get; set; }

        public Nomina() { }

        public Nomina(Empresa empresa, Trabajador trabajador, int horasNormales, int horasEspeciales, int totalAPercibir)
        {
            Empresa = empresa;
            Trabajador = trabajador;
            HorasNormales = horasNormales;
            HorasEspeciales = horasEspeciales;
            TotalAPercibir = totalAPercibir;
        }
    }
}
