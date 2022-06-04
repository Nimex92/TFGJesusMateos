using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class WorkGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Worker>? Workers { get; set; } = new List<Worker>();
        public ICollection<WorkShift>? WorkShifts { get; set; } = new List<WorkShift>();
        public ICollection<WorkTask>? Tasks { get; set; } = new List<WorkTask>();
        public ICollection<Places>? Places { get; set; } = new List<Places>();

        public WorkGroup() { }

        public WorkGroup(string name)
        {
            this.Name = name;
        }
    }
}
