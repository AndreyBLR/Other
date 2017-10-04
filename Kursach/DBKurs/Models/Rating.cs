using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Rating
    {
        public Int32 Personal { get; set; }
        public Int32 Comfort { get; set; }
        public Int32 General
        {
            get { return (Personal + Comfort)/2; }
        } 
    }
}
