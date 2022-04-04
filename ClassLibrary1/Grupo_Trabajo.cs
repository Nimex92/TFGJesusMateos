using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Grupo_Trabajo
    {
        public Grupo_Trabajo()
        {

        }
        [Key]
        public int IdGrupo { get; set; }
        public string Turno { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }

        public Grupo_Trabajo(string turno, string h_entr, string h_sal)
        {
            Turno = turno;
            HoraEntrada = h_entr;
            HoraSalida = h_sal;
        }
        public Grupo_Trabajo(int id_grupo, string turno, string h_entr, string h_sal)
        {
            this.IdGrupo = id_grupo;
            this.Turno = turno;
            this.HoraEntrada = h_entr;
            this.HoraSalida = h_sal;
        }
    }
}
