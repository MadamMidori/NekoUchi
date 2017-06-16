using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoUchi.MVC.Model
{
    public class WordView
    {
        #region Properties
        public string Identification { get; set; }

        [Display(Name = "Značenje")]
        public string Meaning { get; set; }

        public string Kanji { get; set; }
        public string Kana { get; set; }

        [Display(Name = "Razina")]
        public string Level { get; set; }

        public string JishoURL { get; set; }
        #endregion

        #region Static methods
        public static List<WordView> CastFromModelWord(List<NekoUchi.Model.Word> words)
        {
            var wordViews = new List<WordView>();
            foreach(var word in words)
            {
                var wordView = new WordView();
                wordView.Identification = word.Identification;
                wordView.Kana = word.Kana;
                wordView.Kanji = word.Kanji;
                wordView.Level = word.Level;
                wordView.Meaning = word.Meaning;
                wordView.JishoURL = word.JishoURL;
                wordViews.Add(wordView);
            }
            return wordViews;
        }        
        #endregion
    }
}
