using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Day
    {
        [Key]
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public bool Enjoyed { get; set; }
        public virtual Calendar BelongCalendar { get; set; }

        public Day()
        {

        }
        public Day(string reason, DateTime date)
        {
            this.Reason = reason;
            this.Date = date;
        }
        public static List<DateTime> GetFechas(List<Day> dateList)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (Day day in dateList)
            {
                dates.Add(day.Date.Date);
            }
            return dates;
        }
    }
}
