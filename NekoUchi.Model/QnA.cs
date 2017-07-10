using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.Model
{
    public abstract class QnA
    {
        public string CourseId { get; set; }
        public string LessonName { get; set; }
        public string Username { get; set; }
        public int OrderNo { get; set; }
        public int Penalty { get; set; }
        public bool Done { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public abstract List<QnA> GenerateQnA(Course course, string username, string lessonName, Quiz quiz);
    }
}
