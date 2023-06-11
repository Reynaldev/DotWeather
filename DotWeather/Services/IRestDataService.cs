using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotWeather.Models;

namespace DotWeather.Services
{
    public interface IRestDataService
    {
        Task<UserLocation> GetLocation(string loc = "Jakarta");
        Task<OpenWeather> GetWeather(double lat = -6.1753942, double lon = 106.827183);
    }
}
