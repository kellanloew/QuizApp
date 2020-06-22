using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApplication.Models
{
    public class QuestionItem
    {
        
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsComplete { get; set; }
        
    }
}
