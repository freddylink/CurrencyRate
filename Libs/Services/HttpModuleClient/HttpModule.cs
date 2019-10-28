using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.HttpModuleClient
{
    public class HttpModule
    {
        public static async Task<string> DownloadData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(new Uri(url));

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}