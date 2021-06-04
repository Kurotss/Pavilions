using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class Pavilion
    {
        public Pavilion()
        {
            Rents = new HashSet<Rent>();
        }

        public int IdPavilion { get; set; }
        public string NumberPavilion { get; set; }
        public int? NumberFloor { get; set; }
        public string StatusPavilion { get; set; }
        public double? Area { get; set; }
        public double? CostOfArea { get; set; }
        public double? ValAddRatio { get; set; }
        public int? IdShopCenter { get; set; }

        public virtual ShoppingCenter IdShopCenterNavigation { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
