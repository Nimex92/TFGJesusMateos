using System;
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
        public string? BelongstoWorkGroups { get; set; }
        public ICollection<WorkGroup>? WorkGroup { get; set; } = new List<WorkGroup>();
        public virtual User User { get; set; }
        public string? Category { get; set; }
        public DateTime? HiringDate { get; set; }
        public string? Nif { get; set; }
        public string? SocialSecurityCard { get; set; }
        

        public Worker(string name, WorkGroup workGroup,User user)
        {
            Name = name;
            User = user;
            WorkGroup.Add(workGroup);
        }
        public Worker(string name, WorkGroup workGroup, User user, string category,DateTime hiringDate,string nif,string socialsecuritynumber)
        {
            Name = name;
            User = user;
            WorkGroup.Add(workGroup);
            Category = category;
            HiringDate = hiringDate;
            Nif = nif;
            SocialSecurityCard = socialsecuritynumber;

        }
        public Worker(int cardNumber, string name, WorkGroup workGroup)
        {
            CardNumber = cardNumber;
            Name = name;
            WorkGroup.Add(workGroup);
        }
    }
}
