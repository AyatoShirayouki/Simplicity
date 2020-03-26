using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.DataContracts.Dtos
{
    public class TaskDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public NameAndIDDto Assignee { get; set; }

        public NameAndIDDto Creator { get; set; }

        public NameAndIDDto Project { get; set; }

        public TaskStatus Status { get; set; }

        public TaskStatus OldStatus { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsExpired { get; set; }

        public bool IsExpiring { get; set; }
    }
}
