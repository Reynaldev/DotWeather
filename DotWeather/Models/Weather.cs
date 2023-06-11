﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeather.Models
{
    public class OpenWeather
    {
        public double? lat { get; set; }
        public double? lon { get; set; }
        public string? timezone { get; set; }
        public Current? current { get; set; }
    }

    public class Current
    {
        public double? temp { get; set; }
        public int? pressure { get; set; }
        public int? humidity { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class Weather
    {
        public int? id { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }
    }
}