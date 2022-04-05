using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Trabajador
    {
        public Trabajador()
        {

        }
        [Key]
        public int numero_tarjeta { get; set; }
        public string nombre { get; set; }
        public virtual Grupo_Trabajo grupo { get; set; }
        public virtual Usuarios usuario { get; set; }

        public Trabajador(string nombre, int grupo_trabajo)
        {
            this.nombre = nombre;
            this.grupo.IdGrupo = grupo_trabajo;
        }
        public Trabajador(string nombre, Grupo_Trabajo grupo,Usuarios user)
        {
            this.nombre = nombre;
            this.grupo = grupo;
            this.usuario = user;
        }
        public Trabajador(string nombre, int grupo, string user)
        {
            this.nombre = nombre;
            this.grupo.IdGrupo = grupo;
            this.usuario.Username = user;
        }
        public Trabajador(int numero_tarjeta, string nombre, Grupo_Trabajo grupo)
        {
            this.numero_tarjeta = numero_tarjeta;
            this.nombre = nombre;
            this.grupo = grupo;
        }
    }
}
