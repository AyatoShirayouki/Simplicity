using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.DataContracts.Dtos.Tasks
{
    public class TaskEditDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AssigneeID { get; set; }

        public int CreatorID { get; set; }

        public int ProjectID { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime DueDate { get; set; }
    }
}
