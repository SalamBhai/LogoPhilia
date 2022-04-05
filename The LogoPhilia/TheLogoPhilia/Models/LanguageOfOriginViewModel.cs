using System;
using System.Collections.Generic;

namespace TheLogoPhilia.Models
{
    public class LanguageOfOriginViewModel
    {
        public int Id{get;set;}
        public ICollection<WordViewModel> Words{get;set;}
        public string LanguageOfOriginName{get;set;}
        public string HistoryAboutIt{get;set;}
        public string InformationOfWordsFromIt{get;set;}
    }
     public class CreateLanguageOfOriginRequestModel
    {
        public string LanguageOfOriginName{get;set;}
        public string HistoryAboutIt{get;set;}
        public string InformationOfWordsFromIt{get;set;}
    }
     public class UpdateLanguageOfOriginRequestModel
    {
        public string LanguageOfOriginName{get;set;}
        public string HistoryAboutIt{get;set;}
        public string InformationOfWordsFromIt{get;set;}
    }
}