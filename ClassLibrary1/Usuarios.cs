using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Usuarios
    {
        public Usuarios()
        {

        }

        [Key]
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool esAdmin { get; set; }


        public Usuarios(int IdUser ,string Username, string Password, bool esAdmin)
        {
            this.IdUser = IdUser;
            this.Username = Username;
            this.Password = Password;
            this.esAdmin = esAdmin;
        }
        public Usuarios(string Username, string Password,bool esAdmin)
        {
            this.Username = Username;
            this.Password = Password;
            this.esAdmin=esAdmin;
        }
        public Usuarios(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
