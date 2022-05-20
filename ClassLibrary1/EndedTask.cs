using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class EndedTask
    {

        public int id { get; set; }
        public WorkTask Task { get; set; }
        public Worker Worker { get; set; }
        public DateTime TaskTaskInit { get; set; }
        public DateTime TaskEnd { get; set; }
        public double TotalTimeUsed { get; set; }
        public bool OnTime { get; set; }

        public EndedTask()
        {

        }
        public EndedTask(WorkTask task, Worker worker, DateTime taskInit, DateTime taskEnd, double totalTimeUsed,bool onTime)
        {
            this.Task = task;
            this.Worker = worker;
            this.TaskTaskInit = taskInit;
            this.TaskEnd = taskEnd;
            this.TotalTimeUsed = totalTimeUsed;
            this.OnTime = onTime;
        }
    }
}
