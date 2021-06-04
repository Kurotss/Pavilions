using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class Rent
    {
        public int IdRent { get; set; }
        public int? IdRenter { get; set; }
        public int? IdPavilion { get; set; }
        public DateTime? StartDateRent { get; set; }
        public DateTime? EndDateRent { get; set; }
        public int? IdStaffer { get; set; }
        public string StatusRent { get; set; }

        public virtual Pavilion IdPavilionNavigation { get; set; }
        public virtual Renter IdRenterNavigation { get; set; }
        public virtual Staffer IdStafferNavigation { get; set; }
    }
}
