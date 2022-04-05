using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
namespace TheLogoPhilia.Interfaces.IServices
{
    public interface ILanguageOfOriginService
    {
       Task<BaseResponse<LanguageOfOriginViewModel>> Create(CreateLanguageOfOriginRequestModel model);
        Task<BaseResponse<LanguageOfOriginViewModel>> Update(UpdateLanguageOfOriginRequestModel model,int Id);
        Task<BaseResponse<LanguageOfOriginViewModel>> Get(int Id);
        Task<BaseResponse<IEnumerable<LanguageOfOriginViewModel>>> Get();
        Task<bool> Delete(int Id);
         Task<BaseResponse<IEnumerable<LanguageOfOriginViewModel>>> Search(string SearchText);
         
    }
}