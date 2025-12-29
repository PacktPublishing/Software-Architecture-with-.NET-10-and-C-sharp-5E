using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainServices.DTOs
{
    public class KeyDisplayPair<K, D>
    {
        public required K Key { get; set; }
        public required D Display { get; set; }
    }
}
