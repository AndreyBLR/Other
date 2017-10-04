using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Party
    {
        public String Date { get; set; }
        public String Name { get; set; }
        public DJ Dj { get; set; }
        public String Description { get; set; }
        public String Style { get; set; }

        public Party()
        {
            Dj = new DJ();
        }
    }
}
