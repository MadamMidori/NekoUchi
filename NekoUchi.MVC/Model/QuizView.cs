using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoUchi.MVC.Model
{
    public class QuizView
    {
        #region Properties
        public string Identification { get; set; }

        [Display(Name="Naziv")]
        public string Name { get; set; }

        [Display(Name="Tip")]
        public string Type { get; set; }

        [Display(Name="Pitanje")]
        public string QuestionField { get; set; }

        [Display(Name="Odgovor")]
        public string AnswerField { get; set; }
        #endregion

        #region Static methods
        public static List<QuizView> CastFromBllQuiz(List<BLL.Quiz> bllQuizzes)
        {
            var result = new List<QuizView>();
            foreach (var bllQuiz in bllQuizzes)
            {
                var quiz = new QuizView();
                quiz.Name = bllQuiz.ModelQuiz.Name;
                quiz.AnswerField = bllQuiz.ModelQuiz.AnswerField;
                quiz.Identification = bllQuiz.Identification;
                quiz.QuestionField = bllQuiz.ModelQuiz.QuestionField;
                quiz.Type = bllQuiz.ModelQuiz.Type;
                result.Add(quiz);
            }
            return result;
        }

        #endregion
    }
}
