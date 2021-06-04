using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class Staffer
    {
        public Staffer()
        {
            Rents = new HashSet<Rent>();
        }

        public int IdStaffer { get; set; }
        public string Surname { get; set; }
        public string NameStaffer { get; set; }
        public string Middlename { get; set; }
        public string NumberTelephon { get; set; }
        public string Paul { get; set; }
        public byte[] Photo { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
