using DotWeather.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<UserLocation> GetLocation(string loc = "Jakarta")
        {
            UserLocation location = new UserLocation();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/geo/1.0/direct?q={loc}&limit=5&&appid=18ccbbd129b7bdecaaf072a9f9977f01");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Connection failed");
                    return location;
                }

                string content = await response.Content.ReadAsStringAsync();
                location = JsonSerializer.Deserialize<UserLocation>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return location;
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
