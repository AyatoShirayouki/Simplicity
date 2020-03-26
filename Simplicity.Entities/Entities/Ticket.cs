using Simplicity.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Simplicity.DataContracts;

namespace Simplicity.Entities
{
    public class Ticket:BaseEntitity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int AssigneeID { get; set; }

        public int CreatorID { get; set; }

        public int ProjectID { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        public virtual User Assignee { get; set; }

        public virtual Project Project { get; set; }

        public virtual User Creator { get; set; }
    }
}
