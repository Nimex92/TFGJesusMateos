using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class WorkTask
    {
        [Key]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ElapsedTime { get; set; }

        public ICollection<WorkGroup> WorkGroups { get; set; }

        public WorkTask()
        {
            
        }
        public WorkTask(string name,string description,double elapsedTime)
        {
            this.Name = name;
            this.Description = description;
            this.ElapsedTime = elapsedTime;
        }
    }
}
