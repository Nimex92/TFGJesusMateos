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
        public int NumeroTarjeta { get; set; }
        public string Nombre { get; set; }
        public string PerteneceATurnos { get; set; }
        public ICollection<EquipoTrabajo> Equipo { get; set; } = new List<EquipoTrabajo>();
        public virtual Usuarios Usuario { get; set; }
        public string Dni { get; set; }
        public string NumeroSeguridadSocial { get; set; }
        public DateTime FechaDeContratacion { get; set; }
        public string Categoria { get; set; }


        public Trabajador(string nombre, EquipoTrabajo grupo,Usuarios user)
        {
            this.Nombre = nombre;
            this.Usuario = user;
            FechaDeContratacion = DateTime.Now;
        }
        public Trabajador(int numero_tarjeta, string nombre, EquipoTrabajo grupo)
        {
            this.NumeroTarjeta = numero_tarjeta;
            this.Nombre = nombre;
            FechaDeContratacion = DateTime.Now;
        }
        public Trabajador(string nombre, EquipoTrabajo grupo,Usuarios user, string dni,string numeroSeguridadSocial,string categoria)
        {
            this.Nombre = nombre;
            this.Dni = dni;
            this.Usuario = user;
            this.NumeroSeguridadSocial = numeroSeguridadSocial;
            this.Categoria = categoria;
            this.FechaDeContratacion = DateTime.Now;
        }
    }
}
