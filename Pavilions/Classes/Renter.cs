using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class Renter
    {
        public Renter()
        {
            Rents = new HashSet<Rent>();
        }

        public int IdRenter { get; set; }
        public string NameRenter { get; set; }
        public string NumberTelephon { get; set; }
        public string AddressRenter { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
