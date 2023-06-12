using DotWeather.Models;
using System.Diagnostics;
using System.Text.Json;

namespace DotWeather.Services
{
    public class DataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;

        public DataService()
        {
            _httpClient = new HttpClient();
            _url = "https://api.openweathermap.org";
        }

        public async Task<List<UserLocation>> GetAnyLocation(string loc)
        {
            List<UserLocation> location = new List<UserLocation>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/geo/1.0/direct?q={loc}&limit=5&&appid=18ccbbd129b7bdecaaf072a9f9977f01");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Connection failed");
                    return location;
                }

                string content = await response.Content.ReadAsStringAsync();
                location = JsonSerializer.Deserialize<List<UserLocation>>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return location;
        }

        public async Task<UserLocation> GetLocation(string loc = "Jakarta")
        {
            List<UserLocation> location = new List<UserLocation>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/geo/1.0/direct?q={loc}&limit=5&&appid=18ccbbd129b7bdecaaf072a9f9977f01");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Connection failed");
                    return location[0];
                }

                string content = await response.Content.ReadAsStringAsync();
                location = JsonSerializer.Deserialize<List<UserLocation>>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return location[0];
        }

        public async Task<OpenWeather> GetWeather(double lat = -6.1753942, double lon = 106.827183)
        {
            OpenWeather weather = new OpenWeather();

            try 
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/data/3.0/onecall?lat={lat}&lon={lon}&appid=18ccbbd129b7bdecaaf072a9f9977f01");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Connection failed");
                    return weather;
                }

                string content = await response.Content.ReadAsStringAsync();
                weather = JsonSerializer.Deserialize<OpenWeather>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return weather;
        }
    }
}
