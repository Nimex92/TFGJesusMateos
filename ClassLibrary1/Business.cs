using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Business
    {
        public int Id { get; set; }
        public string CIF { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string BankAccountCode { get; set; }

        public Business() {}

        public Business(string cif, string name, string adress,string codigo)
        {
            this.CIF = cif;
            this.Name = name;
            this.Adress = adress;
            this.BankAccountCode = codigo;
        }
        

    }
}
