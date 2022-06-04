using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class WorkShift
    {
        [Key]
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Domingo { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
        public ICollection<WorkGroup>? WorkGroups { get; set; } = new List<WorkGroup>();
        public WorkShift()
        {

        }
        public WorkShift(string name,DateTime checkIn,DateTime checkOut,bool monday,bool tuesday,bool wednesday,bool thursday,bool friday,bool saturday,bool sunday)
        {
            this.Name = name;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
            this.Monday = monday;
            this.Tuesday = tuesday;
            this.Wednesday = wednesday;
            this.Thursday = thursday;
            this.Friday = friday;
            this.Saturday = saturday;
            this.Domingo = sunday;
        }
    }
}
