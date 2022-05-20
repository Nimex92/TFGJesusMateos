﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Worker
    {
        public Worker()
        {

        }
        [Key]
        public int CardNumber { get; set; }
        public string Name { get; set; }
        public string BelongsToWorkShifts { get; set; }
        public ICollection<WorkGroup> WorkGroup { get; set; } = new List<WorkGroup>();
        public virtual User User { get; set; }
        

        public Worker(string name, WorkGroup workGroup,User user)
        {
            Name = name;
            User = user;
            WorkGroup.Add(workGroup);
        }
        public Worker(int cardNumber, string name, WorkGroup workGroup)
        {
            CardNumber = cardNumber;
            Name = name;
            WorkGroup.Add(workGroup);
        }
    }
}