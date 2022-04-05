using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class LanguageOfOriginService : ILanguageOfOriginService
    {
        private readonly ILanguageOfOriginRepository _LanguageOfOriginRepository;

        public LanguageOfOriginService(ILanguageOfOriginRepository languageOfOriginRepository)
        {
            _LanguageOfOriginRepository = languageOfOriginRepository;
        }

        public async Task<BaseResponse<LanguageOfOriginViewModel>> Create(CreateLanguageOfOriginRequestModel model)
        {
           var languageOfOriginExist =  _LanguageOfOriginRepository.AlreadyExists(L=> L.LanguageOfOriginName == model.LanguageOfOriginName);
            if(languageOfOriginExist) return new BaseResponse<LanguageOfOriginViewModel>
            {
                Message = "Failed To Create! Because One Already Exists",
                Success = false,

            };
            var languageOfOrigin = new LanguageOfOrigin 
            {
                
               LanguageOfOriginName = model.LanguageOfOriginName,
               InformationOfWordsFromIt = model.InformationOfWordsFromIt,
               HistoryAboutIt = model.HistoryAboutIt,

            };
            await _LanguageOfOriginRepository.Create(languageOfOrigin);
            return new BaseResponse<LanguageOfOriginViewModel>
            {
               Message = "Successful Registration",
               Success = true,
               Data = new LanguageOfOriginViewModel
               {
                   Id = languageOfOrigin.Id,
                   HistoryAboutIt = languageOfOrigin.HistoryAboutIt,
                   InformationOfWordsFromIt = languageOfOrigin.InformationOfWordsFromIt,
                   LanguageOfOriginName = languageOfOrigin.LanguageOfOriginName,
               }
            };
        }

        public async Task<bool> Delete(int Id)
        {
           var languageOfOrigin =await _LanguageOfOriginRepository.GetLanguageOfOrigin(Id);
           if(languageOfOrigin==null) return false;
           languageOfOrigin.IsDeleted = true;
           _LanguageOfOriginRepository.SaveChanges();
           return true;
        }

        public async Task<BaseResponse<LanguageOfOriginViewModel>> Get(int Id)
        {
           var languageOfOrigin = await _LanguageOfOriginRepository.GetLanguageOfOrigin(Id);
           if(languageOfOrigin == null) return new BaseResponse<LanguageOfOriginViewModel>
           {
                 Message = "Fetch Error!",
                  Success = false,
           };
           return new BaseResponse<LanguageOfOriginViewModel>
           {
             Message = "Retrieval Successful",
             Success = true,
             Data = new LanguageOfOriginViewModel
             {
                 Id = languageOfOrigin.Id,
                 HistoryAboutIt = languageOfOrigin.HistoryAboutIt,
                 LanguageOfOriginName = languageOfOrigin.LanguageOfOriginName,
                 InformationOfWordsFromIt = languageOfOrigin.InformationOfWordsFromIt,
                 Words = languageOfOrigin.Words.Select(L=> new WordViewModel
                 {
                    TheWord = L.TheWord,
                     TheWordId = L.Id,
                     AmericanEnglishAlternateSpelling = L.AmericanEnglishAlternateSpelling,
                     BritishEnglishAlternateSpelling = L.BritishEnglishAlternateSpelling,
                     Meaning = L.Meaning,
                 }).ToList(),
             
             }
           };
        }

        public async Task<BaseResponse<IEnumerable<LanguageOfOriginViewModel>>> Get()
        {
         var languageOfOrigins = await _LanguageOfOriginRepository.GetLanguageOfOrigins();
          if(languageOfOrigins.Count()==0) return new BaseResponse<IEnumerable<LanguageOfOriginViewModel>>
         {
             Success = false,
             Message = "Failed To Fetch!",

         };
         var languageOfOriginsReturned = languageOfOrigins.Select(languageOfOrigin => new LanguageOfOriginViewModel
         {
              Id = languageOfOrigin.Id,
                 HistoryAboutIt = languageOfOrigin.HistoryAboutIt,
                 LanguageOfOriginName = languageOfOrigin.LanguageOfOriginName,
                 InformationOfWordsFromIt = languageOfOrigin.InformationOfWordsFromIt,
                 Words = languageOfOrigin.Words.Select(L=> new WordViewModel
                 {
                    TheWord = L.TheWord,
                     TheWordId = L.Id,
                     AmericanEnglishAlternateSpelling = L.AmericanEnglishAlternateSpelling,
                     BritishEnglishAlternateSpelling = L.BritishEnglishAlternateSpelling,
                     Meaning = L.Meaning,
                 }).ToList(),
         }).ToList();
           return new BaseResponse<IEnumerable<LanguageOfOriginViewModel>>
           {
                   Message = "Successful Retrieval",
                   Success = true,
                   Data  = languageOfOriginsReturned,
           };
        }

        public async Task<BaseResponse<IEnumerable<LanguageOfOriginViewModel>>> Search(string SearchText)
        {
           var searchResult = await _LanguageOfOriginRepository.Search(SearchText);
           return new BaseResponse<IEnumerable<LanguageOfOriginViewModel>>
           {
              Message = "Here Is Your Search Result",
              Success=true,
              Data = searchResult
           };
        }

        public async Task<BaseResponse<LanguageOfOriginViewModel>> Update(UpdateLanguageOfOriginRequestModel model, int Id)
        {
              var languageOfOrigin = await _LanguageOfOriginRepository.GetLanguageOfOrigin(Id);
              if(languageOfOrigin == null) return new BaseResponse<LanguageOfOriginViewModel>
              {
                Message = "Failed To Update",
                Success = true,
              };
              languageOfOrigin.LanguageOfOriginName = languageOfOrigin.LanguageOfOriginName ?? model.LanguageOfOriginName;
              languageOfOrigin.HistoryAboutIt = languageOfOrigin.HistoryAboutIt ?? model.HistoryAboutIt;
               languageOfOrigin.InformationOfWordsFromIt = languageOfOrigin.InformationOfWordsFromIt ?? model.InformationOfWordsFromIt;

               await _LanguageOfOriginRepository.Update(languageOfOrigin);
               
               return new BaseResponse<LanguageOfOriginViewModel>
               {
                  Message = "Successful Update",
                  Success = true,
                  Data = new LanguageOfOriginViewModel
                  {
                  Id = languageOfOrigin.Id,
                  HistoryAboutIt = languageOfOrigin.HistoryAboutIt,
                  InformationOfWordsFromIt = languageOfOrigin.InformationOfWordsFromIt,
                  LanguageOfOriginName = languageOfOrigin.LanguageOfOriginName,
                  Words = languageOfOrigin.Words.Select(L=> new WordViewModel
                 {
                    TheWord = L.TheWord,
                     TheWordId = L.Id,
                     AmericanEnglishAlternateSpelling = L.AmericanEnglishAlternateSpelling,
                     BritishEnglishAlternateSpelling = L.BritishEnglishAlternateSpelling,
                     Meaning = L.Meaning,
                }).ToList(),
                  }
               };

        }
    }
}