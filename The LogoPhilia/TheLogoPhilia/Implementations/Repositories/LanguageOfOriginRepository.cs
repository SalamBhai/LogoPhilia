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
    public class LanguageOfOriginRepository : BaseRepository<LanguageOfOrigin>, ILanguageOfOriginRepository
    {
        public LanguageOfOriginRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<LanguageOfOrigin> GetLanguageOfOrigin(int Id)
        {
            return await _context.LanguageOfOrigins.Include(L=> L.Words).Where( L=> L.Id== Id && L.IsDeleted == false).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<LanguageOfOrigin>> GetLanguageOfOrigins()
        {
           return await _context.LanguageOfOrigins.Include(L=> L.Words).Where(L=> L.IsDeleted == false).ToListAsync();

        }
         public async Task<IEnumerable<LanguageOfOriginViewModel>> Search(string SearchText)
        {
            return  await _context.LanguageOfOrigins.Where(L => EF.Functions.Like(L.LanguageOfOriginName, $"%{SearchText}%")).Select(L=> new LanguageOfOriginViewModel
            {
                LanguageOfOriginName = L.LanguageOfOriginName,
                Id =L.Id,
                HistoryAboutIt = L.HistoryAboutIt,
                InformationOfWordsFromIt = L.InformationOfWordsFromIt,
                Words = L.Words.Select( L=> new WordViewModel
                {
                   TheWordId = L.Id,
                   TheWord = L.TheWord,
                   LanguageOfOriginId = L.LanguageOfOriginId,
                   AmericanEnglishAlternateSpelling = L.AmericanEnglishAlternateSpelling,
                   Meaning = L.Meaning,
                   BritishEnglishAlternateSpelling = L.BritishEnglishAlternateSpelling,
                }).ToList(),

            }).ToListAsync();
        }
    }
}