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
        public virtual ICollection<EquipoTrabajo> equipo { get; set; }
        public virtual Usuarios usuario { get; set; }
        

        public Trabajador(string nombre, int grupo_trabajo)
        {
            this.nombre = nombre;
            
        }
        public Trabajador(string nombre, EquipoTrabajo grupo,Usuarios user)
        {
            this.nombre = nombre;
            this.usuario = user;
        }
        public Trabajador(string nombre, int grupo, string user)
        {
            this.nombre = nombre;
            this.usuario.Username = user;
        }
        public Trabajador(int numero_tarjeta, string nombre, EquipoTrabajo grupo)
        {
            this.numero_tarjeta = numero_tarjeta;
            this.nombre = nombre;
        }
    }
}
