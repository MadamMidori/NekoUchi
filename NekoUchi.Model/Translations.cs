using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.Model
{
    public class Translations
    {
        public Dictionary<string, string> WordFieldTranslations = new Dictionary<string, string>() {
            {"Meaning", "Značenje" },
            {"Kanji", "Kanji" },
            {"Kana", "Kana" },
            {"Image", "Slika" },
            {"Audio", "Zvuk" }
        };

        public Dictionary<string, string> PrijevodiPoljaRijeci = new Dictionary<string, string>()
        {
            {"Značenje", "Meaning" },
            {"Kanji", "Kanji" },
            {"Kana", "Kana" },
            {"Slika", "Image" },
            {"Zvuk", "Audio" }
        };
    }
}
