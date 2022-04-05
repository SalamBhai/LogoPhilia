using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IWordRepository : IRepository<Word>
    {
       Task<Word> GetWord(int Id);
       Task<IEnumerable<Word>> GetWords();
      Task<IEnumerable<WordViewModel>> Search(string SearchText);
    }
}