using System;
using System.Net.Http;
using System.Configuration;

namespace TPInteg_UI.Services.Base
{
    public abstract class BaseApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        protected BaseApiService()
        {
            _baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "https://localhost:7176/api/";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }
    }
}