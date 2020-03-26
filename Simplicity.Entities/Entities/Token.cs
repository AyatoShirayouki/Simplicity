using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Simplicity.Entities.Entities
{
    public class Token : BaseEntitity
    {
        [Required]
        public string Value { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
