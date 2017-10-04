using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class TestingResult
    {
        public List<Question> Question { get; set; }

        public String Subject { get; set; }

        public int NumberVersion { get; set; }

        public Question this[Int32 i]
        {
            get
            {
                return Question[i];
            }
        }

        public TestingResult()
        {
            Question = new List<Question>();
        }
    }
}
