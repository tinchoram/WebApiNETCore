using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TPInteg_UI.Services
{
    public class WebAPICoreService
    {
        static readonly HttpClient client = Client.GetClient();
        public string UrlAddress { get; set; }
        public async Task<string> CallEndpoint()
        {
            try
            {
                var response = await client.GetAsync(UrlAddress);
                if (response.IsSuccessStatusCode)
                {
                    var readResponse = await response.Content.ReadAsStringAsync();
                    return readResponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class Client
        {
            public static HttpClient GetClient()
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                httpClientHandler.UseDefaultCredentials = true;
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                return httpClient;
            }
        }
    }
}