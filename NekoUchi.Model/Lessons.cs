using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.Model
{
    public class Lesson
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Word> Words { get; set; }
        public List<Sentence> Sentences { get; set; }
        public LessonStatistics Statistics { get; set; }
        public List<Quiz> CustomQuizzes { get; set; }
    }

    public class LessonStatistics
    {
        public int NumberOfTestsSolved { get; set; }
        public int NumberOfTestsInLesson { get; set; }
        public List<Word> Top5Hardest { get; set; }
        public List<string> Top5Users { get; set; }
    }
}
