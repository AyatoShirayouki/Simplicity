using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Entities
{
    public class Project:BaseEntitity
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public virtual ICollection<UserProject> UsersProjects { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
