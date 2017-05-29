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
    }
}
