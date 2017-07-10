using NekoUchi.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.BLL
{
    public class Quiz
    {
        #region Properties
        public Model.Quiz ModelQuiz { get; set; }
        public string Identification { get; set; }
        public List<Model.QnA> QnAs { get; set; }
        #endregion

        #region Static methods
        public static List<Quiz> GetAllGenericQuizzes()
        {
            try
            {
                var result = new List<Quiz>();
                IDataProvider data = new MongoDataProvider();
                var quizzes = data.GetMultiple<DAL.Quiz>("", "");
                foreach (var quiz in quizzes)
                {
                    var bllQuiz = new Quiz();
                    bllQuiz.Identification = quiz._id.ToString();
                    bllQuiz.ModelQuiz = quiz;
                    result.Add(bllQuiz);
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Model.QnA> GenerateQnA(string quizId, string courseId, string lessonName, string username)
        {
            var result = new List<Model.QnA>();
            try
            {
                IDataProvider data = new MongoDataProvider();
                var course = data.Get<DAL.Course>("_id", courseId);

                var quiz = data.Get<DAL.Quiz>("_id", quizId);

                Model.QnA source = null;
                if (quiz.Type == "Upisivanje")
                {
                    source = new Model.QnAUpisivanje();                    
                }
                else if (quiz.Type == "Izbor")
                {
                    source = new Model.QnAIzbor();                    
                }
                else if (quiz.Type == "VišestrukiIzbor")
                {
                    source = new Model.QnAVisestruki();
                }
                else
                {
                    result = null;
                }
                return source.GenerateQnA(course, username, lessonName, quiz);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static bool SaveQnAs(List<Model.QnA> qnas)
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                if (data.CreateMany(qnas) == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Model.QnA> Shuffle(List<Model.QnA> listToShuffle)
        {
            for (int i = 1; i < listToShuffle.Count + 1; i++)
            {
                var rnd = new Random();
                int j = rnd.Next(0, listToShuffle.Count);
                while (true)
                {
                    if (listToShuffle[j].OrderNo == 0)
                    {
                        listToShuffle[j].OrderNo = i;
                        break;
                    }
                    else
                    {
                        j = rnd.Next(0, listToShuffle.Count);
                    }
                }                
            }
            return listToShuffle;
        }

        public static Model.QnA GetQnA(string courseId, string lessonName, string username, int orderNo)
        {
            return DAL.Quiz.GetQnA(courseId, lessonName, username, orderNo);
        }
        #endregion
    }
}
