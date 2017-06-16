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

        public List<Model.Word> ModelWords { get; set; }

        #endregion

        #region Static Methods
        public static Word GetAllWords()
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var word = new Word();
                var enumerableWords = data.GetMultiple<DAL.Word>("", "");
                word.ModelWords = new List<Model.Word>();
                foreach (var enumerableWord in enumerableWords)
                {
                    enumerableWord.Identification = enumerableWord._id.ToString();
                    word.ModelWords.Add(enumerableWord);
                }
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
