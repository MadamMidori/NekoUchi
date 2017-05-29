using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.Model
{
    public class Course
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public List<string> Subscribed { get; set; }
        public List<Lesson> Lessons { get; set; }
        public CourseStatistics Statistics { get; set; }
    }

    public class CourseStatistics
    {
        public int NumberOfSubscribers { get; set; }
    }
}
