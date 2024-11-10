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
    public class ProductoService : BaseApiService
    {
        private const string API_ENDPOINT = "Producto";

        public async Task<ServiceResult<List<ProductoDTO>>> GetProductosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(API_ENDPOINT);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var productos = JsonConvert.DeserializeObject<List<ProductoDTO>>(jsonString);
                    return new ServiceResult<List<ProductoDTO>>
                    {
                        Data = productos,
                        Success = true
                    };
                }

                return new ServiceResult<List<ProductoDTO>>
                {
                    Success = false,
                    Message = $"Error del servidor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<List<ProductoDTO>>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<ProductoDTO>>
                {
                    Success = false,
                    Message = "Error inesperado al obtener los productos",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<ProductoDTO>> GetProductoByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_ENDPOINT}/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var producto = JsonConvert.DeserializeObject<ProductoDTO>(jsonString);
                    return new ServiceResult<ProductoDTO>
                    {
                        Data = producto,
                        Success = true
                    };
                }

                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = $"Error del servidor: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = "Error al obtener el producto",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<bool>> DeleteProductoAsync(int id)
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
                        Message = "Producto eliminado exitosamente"
                    };
                }

                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = $"Error al eliminar el producto: {response.StatusCode}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Error al eliminar el producto",
                    Exception = ex
                };
            }
        }
    }
}