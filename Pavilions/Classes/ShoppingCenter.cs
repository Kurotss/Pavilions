using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class ShoppingCenter
    {
        public ShoppingCenter()
        {
            Pavilions = new HashSet<Pavilion>();
        }

        public int IdShopCenter { get; set; }
        public string NameCenter { get; set; }
        public string StatusCenter { get; set; }
        public int CountOfPavilions { get; set; }
        public string City { get; set; }
        public int CostOfConstruction { get; set; }
        public int Floors { get; set; }
        public double? ValAddRatio { get; set; }
        public byte[] ImageCenter { get; set; }

        public virtual ICollection<Pavilion> Pavilions { get; set; }
    }
}
