using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Editor_LibreLingo
{
    public class Lesson
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string AcceptedWords { get; set; }
        public List<string> OtherAcceptedWords { get; set; }
        public List<string> ListOfImageNames { get; set; }
        public string Phrase { get; set; }
        public string Translation { get; set; }
        public List<string> AlternateTranslation { get; set; }

        public Lesson(string name, int type, string acceptedWords, List<string> otherAcceptedWords, List<string> listOfImageNames)
        {
            Name = name;
            Type = type;
            AcceptedWords = acceptedWords;
            OtherAcceptedWords = otherAcceptedWords;
            ListOfImageNames = listOfImageNames;
        }
    }
}
