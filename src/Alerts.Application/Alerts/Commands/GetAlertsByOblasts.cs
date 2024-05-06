namespace Alerts.Application.Alerts.Commands;

public class GetAlertsByOblasts
{
    private readonly string _url;

    public GetAlertsByOblasts(string url)
    {
        _url = url;
    }
    
    public async Task<string> InvokeAsync()
    {
        using (HttpClient httpClient = new())
        {
            var request = await httpClient.GetAsync(_url);

            var content = await request.Content.ReadAsStringAsync();

            return content;
        }
    }
}