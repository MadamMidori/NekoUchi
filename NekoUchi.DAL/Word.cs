using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NekoUchi.DAL
{
    public class Word : Model.Word, ISerializationHelper
    {
        public ObjectId _id { get; set ; }

        public static Model.Word RemoveId(Model.Word word)
        {
            Model.Word newWord = new Model.Word();
            newWord.JishoURL = word.JishoURL;
            newWord.Kana = word.Kana;
            newWord.Kanji = word.Kanji;
            newWord.Level = word.Level;
            newWord.Meaning = word.Meaning;
            return newWord;
        }
    }
}
