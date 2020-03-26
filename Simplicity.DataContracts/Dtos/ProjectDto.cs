using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.DataContracts.Dtos
{
    public class ProjectDto : BaseDto
    {
        public string Name { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<NameAndIDDto> AssignedUsers { get; set; }
    }
}
