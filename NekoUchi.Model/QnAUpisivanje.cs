using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NekoUchi.Model
{
    public class QnAUpisivanje : QnA
    {
        public override List<QnA> GenerateQnA(Course course, string username, string lessonName, Quiz quiz)
        {
            var qnAs = new List<QnA>();
            var lesson = course.Lessons.Find(l => l.Name == lessonName);

            // Words are components of lesson in question
            if (quiz.LessonComponent == "Riječi")
            {
                foreach (var word in lesson.Words)
                {
                    var qna = new QnAUpisivanje();
                    qna.CourseId = course.Identification;
                    qna.LessonName = lessonName;
                    qna.Username = username;

                    var translations = new Translations();
                    string questionField = translations.PrijevodiPoljaRijeci[quiz.QuestionField];
                    string answerField = translations.PrijevodiPoljaRijeci[quiz.AnswerField];

                    qna.Question = (string)word.GetType().GetProperty(questionField).GetValue(word, null);
                    qna.Answer = (string)word.GetType().GetProperty(answerField).GetValue(word, null);
                    qnAs.Add(qna);
                }
            }
            // Sentences are components of lesson in question
            else if (quiz.LessonComponent == "Rečenice")
            {
                foreach (var sentence in lesson.Sentences)
                {
                    // To do...
                }                
            }
            // This is not a valid component
            else
            {
                qnAs = null;
            }

            return qnAs;
        }
    }
}
