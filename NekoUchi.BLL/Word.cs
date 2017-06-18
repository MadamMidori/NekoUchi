using NekoUchi.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.BLL
{
    public class Word
    {
        #region Properties

        public Model.Word ModelWord { get; set; }        

        public string Identification { get; set; }
        #endregion

        #region Static Methods
        public static List<Word> GetAllWords()
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var enumerableWords = data.GetMultiple<DAL.Word>("", "");
                var words = new List<Word>();
                foreach (var enumerableWord in enumerableWords)
                {
                    var word = new Word();
                    word.Identification = enumerableWord._id.ToString();
                    word.ModelWord = enumerableWord;
                    words.Add(word);
                }
                return words;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Word GetWord(string wordId)
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var word = new Word();
                word.ModelWord = data.Get<DAL.Word>("_id", wordId);
                return word;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
