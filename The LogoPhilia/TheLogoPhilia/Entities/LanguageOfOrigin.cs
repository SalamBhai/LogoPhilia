using System.Collections.Generic;

namespace TheLogoPhilia.Entities
{
    public class LanguageOfOrigin : BaseEntity
    {

        public ICollection<Word> Words{get;set;}
        public string LanguageOfOriginName{get;set;}
        public string HistoryAboutIt{get;set;}
        public string InformationOfWordsFromIt{get;set;}
    }
}