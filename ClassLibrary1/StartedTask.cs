using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class StartedTask
    {
        public int id { get; set; } 
        public WorkTask Task { get; set; }   
        public Worker Worker { get; set; }   
        public DateTime TastStart { get; set; }

        public StartedTask()
        {

        }
        public StartedTask(WorkTask task,Worker worker,DateTime taskStart)
        {
            this.Task = task;
            this.Worker = worker;
            this.TastStart = taskStart;
        }
    }
}
