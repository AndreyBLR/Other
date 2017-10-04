using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Question
    {
        public List<Answer> Answers { get; set; }

        public List<Answer> StudAnswers { get; set; }

        public String Part { get; set; }

        public String Text { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
            StudAnswers = new List<Answer>();
        }

        public bool IsTrue
        {
            get
            {
                return Compare(Answers, StudAnswers);
            }
        }

        private bool Compare(List<Answer> answer, List<Answer> stud)
        {
            var temp = from a in answer where a.IsTrue == true select a;
            if (temp.Count() == stud.Count)
            {
                int ind = 0;
                foreach (var ans in temp)
                {
                    if (ans.Text.Trim() != stud[ind].Text.Trim())
                    {
                        return false;
                    }
                    ind++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    
    }
}
