using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sylvie.Helper
{
    public class UUIDFetcher
    {
        public static async Task<string> GetUUID(string name) {
            
            string json;
            using (var client = new HttpClient())
            {
                json = await client.GetStringAsync($"https://api.mojang.com/users/profiles/minecraft/{name}");
            }

            var datas = JsonConvert.DeserializeObject<ProfileResponse>(json);
            if (datas is null) return null;                                   

            var uuid = new StringBuilder();

            for(var i = 0; i < datas.id.Length; i++) {
                uuid.Append(datas.id[i]);
                if(i is 7 or 11 or 15 or 19) {                    
                    uuid.Append("-");
                }
            }

            return uuid.ToString();
        }
    }

    public class ProfileResponse
    {
        public string name {get;set;}
        public string id {get;set;}
    }
}