using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.Model
{
    public class Quiz
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string LessonComponent { get; set; }
        public string QuestionField { get; set; }
        public string AnswerField { get; set; }
    }
}
