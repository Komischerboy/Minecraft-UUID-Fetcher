using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UUIDFetcher;

public class UUIDFetcher
{
    public static async Task<string> GetUUID(string name) {
        
        using var client = new HttpClient();
        var json = await client.GetStringAsync($"https://api.mojang.com/users/profiles/minecraft/{name}");

        if (Convert.ToString(JsonDocument.Parse(json).RootElement.GetProperty("id")) is not { } id) throw new Exception("Player not Found!");
        

        var uuid = new StringBuilder();
        for (var i = 0; i < id.Length; i++) {
            uuid.Append(id[i]);
            if (i is 7 or 11 or 15 or 19) uuid.Append("-");
        }

        return uuid.ToString();
    }
}