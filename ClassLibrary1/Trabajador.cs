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
        private int id;
        private string username;
        private string password;

        public Trabajador()
        {

        }
        [Key]
        public int numero_tarjeta { get; set; }
        public string nombre { get; set; }
        public virtual Grupo_Trabajo grupo { get; set; }

        public Trabajador(string nombre, int grupo_trabajo)
        {
            this.nombre = nombre;
            this.grupo.IdGrupo = grupo_trabajo;
        }
        public Trabajador(int numero_tarjeta, string nombre, Grupo_Trabajo grupo)
        {
            this.numero_tarjeta = numero_tarjeta;
            this.nombre = nombre;
            this.grupo = grupo;
        }

        public Trabajador(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
    }
}
