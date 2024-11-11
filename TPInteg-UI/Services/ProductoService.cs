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

        public async Task<ServiceResult<ProductoDTO>> CreateProductoAsync(ProductoDTO producto)
        {
            try
            {
                // Asegurarse de que el estado tenga un valor por defecto
                if (string.IsNullOrEmpty(producto.estado))
                {
                    producto.estado = "activo";
                }

                // Formatear la fecha en el formato requerido (yyyy-MM-dd)
                producto.fechaAlta = producto.fechaAlta?.Date;

                // Serializar el objeto producto a JSON con el formato adecuado
                var jsonSettings = new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd" // Formato de fecha requerido
                };
                var jsonContent = JsonConvert.SerializeObject(producto, jsonSettings);

                // Crear el contenido del request
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud POST
                var response = await _httpClient.PostAsync(API_ENDPOINT, content);

                // Leer la respuesta
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var createdProducto = JsonConvert.DeserializeObject<ProductoDTO>(jsonString);
                    return new ServiceResult<ProductoDTO>
                    {
                        Data = createdProducto,
                        Success = true,
                        Message = "Producto creado exitosamente"
                    };
                }

                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = $"Error al crear el producto: {response.StatusCode} - {jsonString}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = "Error al crear el producto",
                    Exception = ex
                };
            }
        }

        public async Task<ServiceResult<ProductoDTO>> UpdateProductoAsync(int id, ProductoDTO producto)
        {
            try
            {
                // Asegurarse de que el ID en la URL coincida con el ID del producto
                producto.id = id;

                // Serializar el objeto producto a JSON con el formato adecuado
                var jsonSettings = new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    NullValueHandling = NullValueHandling.Ignore // Ignora propiedades nulas al serializar
                };
                var jsonContent = JsonConvert.SerializeObject(producto, jsonSettings);

                // Crear el contenido del request
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud PUT
                var response = await _httpClient.PutAsync($"{API_ENDPOINT}/{id}", content);

                // Leer la respuesta
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var updatedProducto = JsonConvert.DeserializeObject<ProductoDTO>(jsonString);
                    return new ServiceResult<ProductoDTO>
                    {
                        Data = updatedProducto,
                        Success = true,
                        Message = "Producto actualizado exitosamente"
                    };
                }

                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = $"Error al actualizar el producto: {response.StatusCode} - {jsonString}",
                    ErrorCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = "Error de conexión con el servidor",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<ProductoDTO>
                {
                    Success = false,
                    Message = "Error al actualizar el producto",
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
                    Message = "Error al eliminar el producto",
                    Exception = ex
                };
            }
        }
    }
}