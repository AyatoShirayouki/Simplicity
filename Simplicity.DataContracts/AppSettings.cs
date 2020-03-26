using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string Lifetime { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
