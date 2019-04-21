using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Collections;

namespace Services.HttpModuleClient
{
    public class HttpModule
    {

        public async Task<string> GetDataClient(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(new Uri(url));

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(url + "error: " + ex.Message);
            }
        }
    }
}