using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Club:InstanceType
    {
        public List<Party> Parties { get; set; }

        public Club()
        {
            Parties = new List<Party>();
        }
    }
}
