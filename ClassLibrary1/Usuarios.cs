using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? esAdmin { get; set; }



        public User(int IdUser ,string Username, string Password, bool esAdmin)
        {
            this.IdUser = IdUser;
            this.Username = Username;
            this.Password = Password;
            this.esAdmin = esAdmin;
        }
        public User(string Username, string Password,bool esAdmin)
        {
            this.Username = Username;
            this.Password = Password;
            this.esAdmin=esAdmin;
        }
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
