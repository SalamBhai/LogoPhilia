using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class WordRepository : BaseRepository<Word>, IWordRepository
    {
        public WordRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Word> GetWord(int Id)
        {
           return  await _context.Words.Include(L=> L.LanguageOfOrigin).SingleOrDefaultAsync(L=> L.Id == Id && L.IsDeleted == false);
        }

        public async Task<IEnumerable<Word>> GetWords()
        {
            return await _context.Words.Include(L=> L.LanguageOfOrigin).Where(L=> L.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<WordViewModel>> Search(string SearchText)
        {
            return  await _context.Words.Where(L => EF.Functions.Like(L.TheWord, $"%{SearchText}%")).Select(word=> new WordViewModel
            {
                TheWordId = word.Id,
                TheWord = word.TheWord,
                AmericanEnglishAlternateSpelling = word.AmericanEnglishAlternateSpelling,
                BritishEnglishAlternateSpelling = word.BritishEnglishAlternateSpelling,
                Meaning = word.Meaning,
                WordAudio = word.WordAudio,
                LanguageOfOriginName = word.LanguageOfOrigin.LanguageOfOriginName,
            }).ToListAsync();
        }

    }
}