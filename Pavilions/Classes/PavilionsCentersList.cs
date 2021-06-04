using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class PavilionsCentersList
    {
        public string NameCenter { get; set; }
        public string StatusCenter { get; set; }
        public string NumberPavilion { get; set; }
        public int? NumberFloor { get; set; }
        public double? Area { get; set; }
        public string StatusPavilion { get; set; }
        public double? CostOfArea { get; set; }
        public double? ValAddRatio { get; set; }
        public int? IdShopCenter { get; set; }
        public int IdPavilion { get; set; }
    }
}
