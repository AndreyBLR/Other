using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Movie:InstanceType
    {
        public List<Film> Movies{ get; set;}

        public Movie()
        {
            Movies = new List<Film>();
        }
    }
}
