using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TPInteg_UI.DTO;
using static TPInteg_UI.Services.WebAPICoreService;

namespace TPInteg_UI.Services
{
    public static class LocalidadService
    {
        static readonly HttpClient client = Client.GetClient();
        public static List<LocalidadDTO> GetAllLocalidades(string uri)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<LocalidadDTO>>(webClient.DownloadString(uri));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static LocalidadDTO GetLocalidadById(string uri, int id)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    return JsonConvert.DeserializeObject<LocalidadDTO>(webClient.DownloadString($"{uri}/{id}"));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static bool UpdateLocalidad(string uri, LocalidadDTO data)
        {
            string json = JsonConvert.SerializeObject(data);
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var responseString = webClient.UploadString(uri, "PATCH", json);
                    return Convert.ToBoolean(responseString);
                }
                catch (Exception)
                {
                    return false;                    
                }
            }
        }
        public static bool DeleteLocalidad(string uri, int id)
        {            
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(id.ToString());
                    var responseString = webClient.UploadData($"{uri}/{id}", "DELETE", dataBytes);
                    return Convert.ToBoolean(responseString);
                }
                catch (Exception)
                {
                    return false;                     
                }
            }
        }
    }
}