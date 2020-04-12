using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.DataContracts.Dtos.Projects
{
    public class ProjectEditDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int[] AssignedUsers { get; set; }
    }
}
