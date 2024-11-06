using System;
using System.Net.Http;

namespace TPInteg_UI
{
    public static class Client
    {
        public static HttpClient GetClient()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            httpClientHandler.UseDefaultCredentials = true;
            HttpClient httpClient = new HttpClient(httpClientHandler);
            httpClient.Timeout = TimeSpan.FromMinutes(10);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            return httpClient;
        }
    }
}