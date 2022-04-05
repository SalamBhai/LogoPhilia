
using System.IO;
using System.Threading.Tasks;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class OxfordService : IOxfordService
    {
        public string ConnectToOxford(string word)
        {
            string wordId= word.ToLower();
            string languageCode = "en";
             string strictMatch = "true";
             string url = $"https://od-api.oxforddictionaries.com/api/v2/entries/{languageCode}/{wordId}?&strictMatch={strictMatch}";
            try
            {
                var webRequest = System.Net.WebRequest.Create(url);
                if(webRequest!=null)
                {
                    webRequest.Method= "Get";
                    webRequest.Timeout= 100000000;
                    webRequest.ContentType= "application/json";
                    webRequest.Headers.Add("app_id","ea16e016");
                    webRequest.Headers.Add("app_key","4db44b52bdac4dcd86d21089db7bd901");
                }
                using(System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse= sr.ReadToEnd();
                        return jsonResponse;
                    }
                }
            }
            catch (System.Exception ex)
            {
                  throw ex;
            }
        }

        public string ConnectToOxfordForAudio(string word)
        {
             string wordId= word.ToLower();
            string languageCode = "en";
             string strictMatch = "true";
             string url = $"https://od-api.oxforddictionaries.com/api/v2/entries/{languageCode}/{wordId}?fields=pronunciations&strictMatch={strictMatch}";
            try
            {
                var webRequest = System.Net.WebRequest.Create(url);
                if(webRequest!=null)
                {
                    webRequest.Method= "Get";
                    webRequest.Timeout= 100000000;
                    webRequest.ContentType= "application/json";
                    webRequest.Headers.Add("app_id","ea16e016");
                    webRequest.Headers.Add("app_key","4db44b52bdac4dcd86d21089db7bd901");
                }
                using(System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse= sr.ReadToEnd();
                        return jsonResponse;
                    }
                }
            }
            catch (System.Exception ex)
            {
                  throw ex;
            }
        }


        
    }
}