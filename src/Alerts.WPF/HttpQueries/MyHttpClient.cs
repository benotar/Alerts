using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Alerts.WPF.HttpQueries;

public class MyHttpClient : IDisposable
{
    private HttpClient _httpClient;
    
    public MyHttpClient()
    {
        Initialize();
    }

    private void Initialize()
    {
        _httpClient = new HttpClient();
        
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private void EnsureHttpClient()
    {
        if (_httpClient is null)
        {
            Initialize();
        }
    }
    
    
    public async Task<T?> GetWithTokenAsync<T>(string url, string token)
    {
        EnsureHttpClient();
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show($"Error :{response.StatusCode}");

                return default;
            }
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"HTTP error: {ex.Message}");

            return default;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");

            return default;
        }

    }
    
    public async Task<T?> PostAsync<T>(string url, object body)
    {
        EnsureHttpClient();
        
        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(url, content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Введено некоректні дані!");

                return default;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"HTTP error: {ex.Message}");

            return default;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");

            return default;
        }
    }
    
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}