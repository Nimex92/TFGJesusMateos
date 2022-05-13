using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Empresa
    {
        public int Id { get; set; }
        public string CIF { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CodigoCuentaMonetaria { get; set; }

        public Empresa() {}

        public Empresa(string cif, string nombre, string direccion,string codigo)
        {
            this.CIF = cif;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.CodigoCuentaMonetaria = codigo;
        }
        

    }
}
