using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Payroll
    {
        public int Id { get; set; }
        public Business Business { get; set; }
        public Worker Worker { get; set; }
        public int NormalHours { get; set; }
        public int SpecialHours { get; set; }
        public int TotalToReceive { get; set; }

        public Payroll() { }

        public Payroll(Business business, Worker worker, int normalHours, int specialHours, int totalToReceive)
        {
            Business = business;
            Worker = worker;
            NormalHours = normalHours;
            SpecialHours = specialHours;
            TotalToReceive = totalToReceive;
        }
    }
}
