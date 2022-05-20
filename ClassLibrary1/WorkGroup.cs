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
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();
        string workers;
        public ICollection<WorkShift> WorkShifts { get; set; } = new List<WorkShift>();
        string workShifts;
        public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
        string tasks;
        public ICollection<Places> Places { get; set; } = new List<Places>();
        string places;

        public WorkGroup() { }

        public WorkGroup(string name)
        {
            this.Name = name;
        }
    }
}
