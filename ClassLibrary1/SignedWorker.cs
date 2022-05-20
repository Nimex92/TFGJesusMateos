using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class SignedWorker
    {
        [Key]
        public int Id { get; set; }
        public Worker Worker { get; set; }
        public Signing Signing { get; set; }

        public SignedWorker()
        {

        }
        public SignedWorker(Worker worker, Signing signing)
        {
            this.Worker = worker;
            this.Signing = signing;
        }
    }
}
