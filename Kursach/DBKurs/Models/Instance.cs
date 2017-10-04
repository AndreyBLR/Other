using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Instance
    {
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Description { get; set; }
        public String WorkingDay { get; set; }
        public Adress Adress { get; set; }
        public String Payment { get; set; }
        public Boolean DiscontCart { get; set; }
        public Rating Rating { get; set; }
        public InstanceType Type { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
