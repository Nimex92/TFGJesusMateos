using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibray
{
    public class Places
    {
        public Places() { }

        [Key]
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public ICollection<WorkGroup>? WorkGroups { get; set; } = new List<WorkGroup>();

        public Places(string name)
        {
            Name = name;
        }

        public Places(string name, WorkGroup workGroup)
        {
            Name = name;
            WorkGroups.Add(workGroup);
        }
    }
}
