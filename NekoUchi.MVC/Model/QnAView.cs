using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoUchi.MVC.Model
{
    public class QnAView
    {
        public string Question { get; set; }
        public int OrderNo { get; set; }
        public string Token { get; set; }
        public string CourseId { get; set; }
        public string LessonName { get; set; }             
    }
}
