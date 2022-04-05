using System.IO;
using System.Threading.Tasks;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IOxfordService
    {
    
      string ConnectToOxford(string word);
       string ConnectToOxfordForAudio(string word);
     
    }
}