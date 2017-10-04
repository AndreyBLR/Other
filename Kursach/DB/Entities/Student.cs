using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
    public class Student
    {
        public String PhoneNumber { get; set; }

        public String InstitutionType { get; set; }

        public PassportData Passport { get; set; }

        public List<TestingResult> Result{ get; set; }

        public TestingResult this[string subject]
        {
            get
            {
                return(from TestingResult result in Result where result.Subject.Trim() == subject select result).FirstOrDefault();
            }
        }
    }
}
