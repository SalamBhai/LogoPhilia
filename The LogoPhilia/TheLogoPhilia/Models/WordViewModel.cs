using System;
using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;
using Microsoft.AspNetCore.Http;

namespace TheLogoPhilia.Models
{
    public class WordViewModel
    {
        public int TheWordId{get;set;}
        public string TheWord{get;set;}
        public string WordAudio{get;set;}
        public string WordAlternatePronunciation{get;set;}
        public string BritishEnglishAlternateSpelling{get;set;}
        public string AmericanEnglishAlternateSpelling{get;set;}
        public string Meaning{get;set;}
        public int LanguageOfOriginId{get;set;}
        public string LanguageOfOriginName{get;set;}
        public string BritishOrAmerican {get;set;}
        
    }
    public class CreateWordRequestModel
    {
        public string BritishOrAmerican {get;set;}
        public string TheWord{get;set;}
        public IFormFile WordAudio{get;set;}
        public string WordAlternatePronunciation{get;set;}
        public string BritishEnglishAlternateSpelling{get;set;}
        public string AmericanEnglishAlternateSpelling{get;set;}
         public string PartOfSpeech {get;set;}
        public string Meaning{get;set;}
        public int LanguageOfOriginId{get;set;}
     
        
    }
    public class UpdateWordRequestModel
    {
        public string TheWord{get;set;}
        public IFormFile WordAudio{get;set;}
        public string WordAlternatePronunciation{get;set;}
        public string BritishEnglishAlternateSpelling{get;set;}
        public string AmericanEnglishAlternateSpelling{get;set;}
        public string Meaning{get;set;}
        
        
    }
}