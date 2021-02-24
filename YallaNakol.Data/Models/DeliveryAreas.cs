using System;
using System.Collections.Generic;
using System.Text;

namespace YallaNakol.Data.Models
{
    [Flags]
    public enum DeliveryAreas
    {
        NewCairo=1,
        Madinaty=2,
        NasrCity=4,
        Maddi=8,
        AinShams=16,
        October=32,
        Obour = 64
    }
}
