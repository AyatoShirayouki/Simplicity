using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Entities
{
    public class UserProject : BaseEntitity
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}
