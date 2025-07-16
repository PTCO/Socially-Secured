using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class Query
{
    public static HttpClient httpClient = new HttpClient();

    public static string GetQuery(string query)
    {
        try
        {
            var result = httpClient.GetAsync(query);
            if (result.Result.IsSuccessStatusCode)
            {
                var content = result.Result.Content.ReadAsStringAsync().Result;
                return content;
            }
            else
            {
                return string.Empty;
            }
        }
        catch (System.Text.Json.JsonException ex)
        {
            return ex.Message;
        }
    }

    public static string PostQueryAsync(string query, string data)
    {
        try
        {
            var contentJson = new StringContent(data, System.Text.Encoding.UTF8, "application/json"); // Ensure the content type is set to application/json
            var results = httpClient.PostAsync(query, contentJson);
            if (results.Result.IsSuccessStatusCode)
            {
                var content = results.Result.Content.ReadAsStringAsync().Result;
                return content;
            }
            else
            {
                return null;
            }
        }
        catch (System.Text.Json.JsonException ex)
        {
            return ex.Message;
        }
    }
} 