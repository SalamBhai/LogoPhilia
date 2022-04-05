using System;
using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Entities
{
    public class Word: BaseEntity
    {
        public string TheWord{get;set;}
        public string WordAudio{get;set;}
        public string PartOfSpeech {get;set;}
        public string BritishOrAmerican {get;set;}
        public string BritishEnglishAlternateSpelling{get;set;}
        public string AmericanEnglishAlternateSpelling{get;set;}
        public string Meaning{get;set;}
        public int LanguageOfOriginId{get;set;}
        public LanguageOfOrigin LanguageOfOrigin{get;set;}
      
       
    }
}