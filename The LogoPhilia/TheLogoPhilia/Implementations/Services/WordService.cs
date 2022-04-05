using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WordService(IWordRepository wordRepository ,IWebHostEnvironment webHostEnvironment)
        {
            _wordRepository = wordRepository;
            _webHostEnvironment= webHostEnvironment;
        }

        public async Task<BaseResponse<WordViewModel>> CreateWord(CreateWordRequestModel model)
        {
           var word = _wordRepository.AlreadyExists(L=> L.TheWord == model.TheWord);
           if(word) return new BaseResponse<WordViewModel>
           {
                 Message = "Word Already Exists",
                 Success = false,
           };
           var wordAudio = "";
           if(model.WordAudio != null)
            {
              string wordAudioPath = Path.Combine(_webHostEnvironment.WebRootPath, "AdminImage");
               Directory.CreateDirectory(wordAudioPath);
               string wordAudioType = model.WordAudio.ContentType.Split('/')[1];
              wordAudio = $"{model.TheWord}/{Guid.NewGuid().ToString().Substring(0,9)}.{wordAudioType}";
                var fullPath = Path.Combine(wordAudioPath,wordAudio);
                if(wordAudioType.ToLower() != "mp3" || wordAudioType.ToLower() != "mpeg" || wordAudioType.ToLower() != "wav")
                {
                  throw new Exception("File Type Not Supported");
                }
               
                else
                {
                    using (var fs = new FileStream(fullPath, FileMode.Create))
                     {
                     model.WordAudio.CopyTo(fs);
                   }
                }  
            }  
           var newWord = new Word
           {
               
                AmericanEnglishAlternateSpelling = model.AmericanEnglishAlternateSpelling,
                BritishEnglishAlternateSpelling = model.BritishEnglishAlternateSpelling,
                LanguageOfOriginId = model.LanguageOfOriginId,
                WordAudio = wordAudio,
                TheWord = model.TheWord,
                Meaning =model.Meaning,
                PartOfSpeech = model.PartOfSpeech,
                BritishOrAmerican = model.BritishOrAmerican
           };
           await _wordRepository.Create(newWord);
           return new BaseResponse<WordViewModel>
           {
             Message = "Successful Creation Of Word",
             Success = true,
             Data = new WordViewModel
             {
                 TheWordId = newWord.Id,
                 TheWord = newWord.TheWord,
                 AmericanEnglishAlternateSpelling = newWord.AmericanEnglishAlternateSpelling,
                 BritishEnglishAlternateSpelling = newWord.BritishEnglishAlternateSpelling,
                 LanguageOfOriginId = newWord.LanguageOfOriginId,
                 LanguageOfOriginName = newWord.LanguageOfOrigin.LanguageOfOriginName,
                 Meaning = newWord.Meaning,
                 WordAudio = newWord.WordAudio,
                 BritishOrAmerican = newWord.BritishOrAmerican,
             }
           };
        }

        public async Task<bool> Delete( int Id)
        {
            var word = await _wordRepository.Get(Id);
            if(word==null) return false;
            word.IsDeleted = true;
            _wordRepository.SaveChanges();
            return true;
            
        }

        public async Task<BaseResponse<IEnumerable<WordViewModel>>> GetAll()
        {
            var words = await _wordRepository.GetWords();
            if (words==null) return new BaseResponse<IEnumerable<WordViewModel>>
            {
                Message = "Error!",
                 Success = false,

            };
            var wordsReturned = words.Select(newWord=> new WordViewModel
            {
                TheWordId = newWord.Id,
                 TheWord = newWord.TheWord,
                 AmericanEnglishAlternateSpelling = newWord.AmericanEnglishAlternateSpelling,
                 BritishEnglishAlternateSpelling = newWord.BritishEnglishAlternateSpelling,
                 LanguageOfOriginId = newWord.LanguageOfOriginId,
                 LanguageOfOriginName = newWord.LanguageOfOrigin.LanguageOfOriginName,
                 Meaning = newWord.Meaning,
                
                 WordAudio = newWord.WordAudio,
                 BritishOrAmerican = newWord.BritishOrAmerican,
             }).ToList();
             return new BaseResponse<IEnumerable<WordViewModel>>
             {
                Message = "Words Retrieved!",
                 Success = true,
                 Data= wordsReturned,
             };
        }

        public  async Task<BaseResponse<WordViewModel>> GetWord( int Id)
        {
          var word = await _wordRepository.GetWord(Id);
            if(word == null) return new BaseResponse<WordViewModel>
            {
               Message= "Error!",
               Success = false,
            };
            return new BaseResponse<WordViewModel>
            {
               Message = "Successful Retrieval",
               Success = true,
               Data = new WordViewModel
               {
                    TheWordId = word.Id,
                 TheWord = word.TheWord,
                 AmericanEnglishAlternateSpelling = word.AmericanEnglishAlternateSpelling,
                 BritishEnglishAlternateSpelling = word.BritishEnglishAlternateSpelling,
                 LanguageOfOriginId = word.LanguageOfOriginId,
                 LanguageOfOriginName = word.LanguageOfOrigin.LanguageOfOriginName,
                 Meaning = word.Meaning,
                 WordAudio = word.WordAudio,
                 BritishOrAmerican = word.BritishOrAmerican
               }
            };
        }

        public async Task<BaseResponse<IEnumerable<WordViewModel>>> Search(string SearchText)
        {
           var searchResult = await _wordRepository.Search(SearchText);
           return new BaseResponse<IEnumerable<WordViewModel>>
            {
                Data =searchResult,
                Message= "Here Is Your Search Result",
                Success = true,

            };
        }

        public async Task<BaseResponse<WordViewModel>> UpdateWord( int Id, UpdateWordRequestModel model)
        {
           var word = await _wordRepository.GetWord(Id);
              var wordAudio = "";
           if(model.WordAudio != null)
            {
              string wordAudioPath = Path.Combine(_webHostEnvironment.WebRootPath, "AdminImage");
               Directory.CreateDirectory(wordAudioPath);
               string wordAudioType = model.WordAudio.ContentType.Split('/')[1];
              wordAudio = $"{model.TheWord}/{Guid.NewGuid().ToString().Substring(0,9)}.{wordAudioType}";
                var fullPath = Path.Combine(wordAudioPath,wordAudio);
                if(wordAudioType.ToLower() != "mp3" || wordAudioType.ToLower() != "mpeg")
                {
                  throw new Exception("File Type Not Supported");
                }
               
                else
                {
                    using (var fs = new FileStream(fullPath, FileMode.Create))
                     {
                     model.WordAudio.CopyTo(fs);
                   }
                }  
            }
           if(word == null) return new BaseResponse<WordViewModel>
           {
              Message ="Retrieval Failed",
              Success = false,
           };
           word.AmericanEnglishAlternateSpelling = word.AmericanEnglishAlternateSpelling ?? model.AmericanEnglishAlternateSpelling;
           word.BritishEnglishAlternateSpelling = word.BritishEnglishAlternateSpelling ?? model.BritishEnglishAlternateSpelling;
           word.WordAudio = wordAudio;
         word.Meaning = word.Meaning ?? model.Meaning;
         word.TheWord = word.TheWord ?? model.TheWord;
           await _wordRepository.Update(word);
           return new BaseResponse<WordViewModel>
           {
               Message = "Update Successful",
               Success =true,
               Data = new WordViewModel
               {
                    TheWordId = word.Id,
                 TheWord = word.TheWord,
                 AmericanEnglishAlternateSpelling = word.AmericanEnglishAlternateSpelling,
                 BritishEnglishAlternateSpelling = word.BritishEnglishAlternateSpelling,
                 LanguageOfOriginId = word.LanguageOfOriginId,
                 LanguageOfOriginName = word.LanguageOfOrigin.LanguageOfOriginName,
                 Meaning = word.Meaning,
                 WordAudio = word.WordAudio,
                 BritishOrAmerican = word.BritishOrAmerican,
               }
           };
        }
    }
}