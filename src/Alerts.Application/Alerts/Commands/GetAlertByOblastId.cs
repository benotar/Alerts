using System.Text;
using Microsoft.Extensions.Primitives;

namespace Alerts.Application.Alerts.Commands;

public class GetAlertByOblastId
{
    private readonly string _url;

    private readonly int _locationId;

    private readonly string _token;

    private readonly string _temp = ".json?token=";
    
    public GetAlertByOblastId(string url, int locationId, string token)
    {
        _url = url;

        _locationId = locationId;

        _token = token;
    }
    
    /// <summary>
    /// Requires a unique oblastId identifier within the URL
    /// </summary>
    /// <returns>Returns the alarm state in the specified area</returns>
    public async Task<string> InvokeAsync()
    {
        using (HttpClient httpClient = new())
        {
            var fullUrlBuilder = new StringBuilder(_url);

            fullUrlBuilder.Append(_locationId);

            fullUrlBuilder.Append(_temp);

            fullUrlBuilder.Append(_token);

            var fullUrl = fullUrlBuilder.ToString();
            
            var responseMessage = await httpClient.GetAsync(fullUrl);
            
            var content = await responseMessage.Content.ReadAsStringAsync();
            
            return content;
        }
    }
}