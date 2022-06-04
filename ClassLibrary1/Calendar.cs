using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Calendar
    {
        public int Id { get; set; }
        public virtual Worker Worker { get; set; }
        public ICollection<Day>? DaysOnCalendar { get; set; } = new List<Day>();

        public Calendar() { }
        public Calendar(Worker trab)
        {
            Worker = trab;
        }
    }
}
