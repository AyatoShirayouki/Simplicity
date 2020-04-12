using System.Collections.Generic;

namespace Simplicity.DataContracts.Dtos
{
    public class UserListDto : BaseDto
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public NameAndIDDto Role { get; set; }

        public List<NameAndIDDto> Projects { get; set; }
    }
}
