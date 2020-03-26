using Simplicity.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Simplicity.ViewModels.Tasks
{
    public class TasksEditVM
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AssigneeID { get; set; }

        public int CreatorID { get; set; }

        public int ProjectID { get; set; }

        public TaskStatus Status { get; set; }
        public TaskStatus OldStatus { get; set; }

        public DateTime DueDate { get; set; }
    }
}
