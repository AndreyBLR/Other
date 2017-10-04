using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class FoodInstanceType:InstanceType
    {
        public String Quisine { get; set; }
        public String Type { get; set; }
        public Int32 Category { get; set; }
        public Int32 SeatAmount { get; set; }
    }
}
