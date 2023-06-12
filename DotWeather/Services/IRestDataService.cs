using DotWeather.Models;

namespace DotWeather.Services
{
    public interface IRestDataService
    {
        Task<List<UserLocation>> GetAnyLocation(string loc);
        Task<UserLocation> GetLocation(string loc = "Jakarta");
        Task<OpenWeather> GetWeather(double lat = -6.1753942, double lon = 106.827183);
    }
}
