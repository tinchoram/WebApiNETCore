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
    public class ProveedorService : BaseApiService
    {
        private const string API_ENDPOINT = "Proveedor";

        public async Task<ServiceResult<List<ProveedorDTO>>> GetProveedoresAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(API_ENDPOINT);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(jsonString);
                    return new ServiceResult<List<ProveedorDTO>>
                    {
                        Data = proveedores,
                        Success = true
                    };
                }

                return new ServiceResult<List<ProveedorDTO>>
                {
                    Success = false,
                    Message = $"Error del servidor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<List<ProveedorDTO>>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<ProveedorDTO>>
                {
                    Success = false,
                    Message = "Error inesperado al obtener los proveedores",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<ProveedorDTO>> GetProveedorByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_ENDPOINT}/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var proveedor = JsonConvert.DeserializeObject<ProveedorDTO>(jsonString);
                    return new ServiceResult<ProveedorDTO>
                    {
                        Data = proveedor,
                        Success = true
                    };
                }

                return new ServiceResult<ProveedorDTO>
                {
                    Success = false,
                    Message = $"Error del servidor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<ProveedorDTO>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<ProveedorDTO>
                {
                    Success = false,
                    Message = "Error al obtener el proveedor",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<ProveedorDTO>> CreateProveedorAsync(ProveedorDTO proveedor)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(proveedor),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(API_ENDPOINT, content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var createdProveedor = JsonConvert.DeserializeObject<ProveedorDTO>(jsonString);
                    return new ServiceResult<ProveedorDTO>
                    {
                        Data = createdProveedor,
                        Success = true,
                        Message = "Proveedor creado exitosamente"
                    };
                }

                return new ServiceResult<ProveedorDTO>
                {
                    Success = false,
                    Message = $"Error al crear el proveedor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<ProveedorDTO>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<ProveedorDTO>
                {
                    Success = false,
                    Message = "Error al crear el proveedor",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<bool>> UpdateProveedorAsync(int id, ProveedorDTO proveedor)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(proveedor),
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
                        Message = "Proveedor actualizado exitosamente"
                    };
                }

                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = $"Error al actualizar el proveedor: {response.StatusCode}",
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
                    Message = "Error al actualizar el proveedor",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<bool>> DeleteProveedorAsync(int id)
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
                        Message = "Proveedor eliminado exitosamente"
                    };
                }

                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = $"Error al eliminar el proveedor: {response.StatusCode}",
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
                    Message = "Error al eliminar el proveedor",
                    Exception = ex
                };
            }
        }
    }
}