using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TPInteg_UI;
using TPInteg_UI.DTO;

public class ProveedorService
{
    private readonly HttpClient _client;

    public ProveedorService()
    {
        _client = new HttpClient { BaseAddress = new Uri("https://localhost:7176/api/") };
    }

    public async Task<List<ProveedorDTO>> GetProveedoresAsync(string searchTerm = "")
    {
        string url = "Proveedor";
        if (!string.IsNullOrEmpty(searchTerm))
        {
            url += $"/nombre/{searchTerm}";
        }

        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<List<ProveedorDTO>>();
        }

        Console.WriteLine(response.ToString());

        return new List<ProveedorDTO>();
    }
}
