using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Issue
    {
        public int Id { get; set; }
        public Worker Worker { get; set; }
        public string IssueReason { get; set; }
        public DateTime IssueDate { get; set; }
        public bool Justified { get; set; }

        public Issue() { }

        public Issue(Worker trab, string motivo, DateTime fecha,bool justified)
        {
            this.Worker=trab;
            this.IssueReason = motivo;
            this.IssueDate = fecha;
            this.Justified=justified;
        }
    }
}
