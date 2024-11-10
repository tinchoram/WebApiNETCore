using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TPInteg_UI.DTO;
using TPInteg_UI.Services.Base;

namespace TPInteg_UI.Services
{
    public class LocalidadService : BaseApiService
    {
        private const string API_ENDPOINT = "Localidad";

        public async Task<ServiceResult<List<LocalidadDTO>>> GetLocalidadesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(API_ENDPOINT);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var localidades = JsonConvert.DeserializeObject<List<LocalidadDTO>>(jsonString);
                    return new ServiceResult<List<LocalidadDTO>>
                    {
                        Data = localidades,
                        Success = true
                    };
                }

                return new ServiceResult<List<LocalidadDTO>>
                {
                    Success = false,
                    Message = $"Error del servidor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<List<LocalidadDTO>>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<LocalidadDTO>>
                {
                    Success = false,
                    Message = "Error inesperado al obtener las localidades",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<LocalidadDTO>> GetLocalidadByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_ENDPOINT}/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var localidad = JsonConvert.DeserializeObject<LocalidadDTO>(jsonString);
                    return new ServiceResult<LocalidadDTO>
                    {
                        Data = localidad,
                        Success = true
                    };
                }

                return new ServiceResult<LocalidadDTO>
                {
                    Success = false,
                    Message = $"Error del servidor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<LocalidadDTO>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<LocalidadDTO>
                {
                    Success = false,
                    Message = "Error al obtener la localidad",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<LocalidadDTO>> CreateLocalidadAsync(LocalidadDTO localidad)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(localidad),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(API_ENDPOINT, content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var createdLocalidad = JsonConvert.DeserializeObject<LocalidadDTO>(jsonString);
                    return new ServiceResult<LocalidadDTO>
                    {
                        Data = createdLocalidad,
                        Success = true,
                        Message = "Localidad creada exitosamente"
                    };
                }

                return new ServiceResult<LocalidadDTO>
                {
                    Success = false,
                    Message = $"Error al crear la localidad: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<LocalidadDTO>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<LocalidadDTO>
                {
                    Success = false,
                    Message = "Error al crear la localidad",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<bool>> UpdateLocalidadAsync(int id, LocalidadDTO localidad)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(localidad),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"{API_ENDPOINT}/{id}", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ServiceResult<bool>
                    {
                        Data = true,
                        Success = true,
                        Message = "Localidad actualizada exitosamente"
                    };
                }

                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = $"Error al actualizar la localidad: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Error al actualizar la localidad",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<bool>> DeleteLocalidadAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{API_ENDPOINT}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return new ServiceResult<bool>
                    {
                        Data = true,
                        Success = true,
                        Message = "Localidad eliminada exitosamente"
                    };
                }

                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = $"Error al eliminar la localidad: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Error al eliminar la localidad",
                    Exception = ex
                };
            }
        }
    }
}