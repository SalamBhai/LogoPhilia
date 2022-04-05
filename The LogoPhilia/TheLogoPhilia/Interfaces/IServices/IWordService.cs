using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IWordService
    {
        Task<BaseResponse<WordViewModel>> CreateWord(CreateWordRequestModel model);
        Task<BaseResponse<IEnumerable<WordViewModel>>> GetAll();
        Task<BaseResponse<IEnumerable<WordViewModel>>> Search(string SearchText);
        Task<BaseResponse<WordViewModel>> UpdateWord(int Id, UpdateWordRequestModel model);
        Task<BaseResponse<WordViewModel>> GetWord(int Id);
        Task<bool> Delete(int Id);

    }
}