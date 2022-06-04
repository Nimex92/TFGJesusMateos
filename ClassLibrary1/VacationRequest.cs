using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class VacationRequest
    {
        [Key]
        public int Id { get; set; }
        public string Worker { get; set; }
        public DateTime Date { get; set; }
        public bool? Accepted { get; set; }

        public VacationRequest()
        {

        }
        public VacationRequest(string worker, DateTime date, bool accepted)
        {
            this.Worker = worker;
            this.Accepted = accepted;
            this.Date = date;
        }
    }
}
