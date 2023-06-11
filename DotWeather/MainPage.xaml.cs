using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using DotWeather.Models;
using DotWeather.Services;

namespace DotWeather;

public partial class MainPage : ContentPage
{
    private readonly DataService _dataService;
    private UserLocation _location;
    private OpenWeather _weather;

    int count = 0;

	public MainPage()
	{
		InitializeComponent();

		_dataService = new DataService();
		_location = new UserLocation();
		_weather = new OpenWeather();
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		_weather = await _dataService.GetWeather();

		TempLabel.Text = $"{ConvertTemp((int)_weather.current.temp)}°C";
	}

	private async void OnLocationChanged(string loc)
	{
		_location = await _dataService.GetLocation(loc);
		_weather = await _dataService.GetWeather((double)_location.lat, (double)_location.lon);

        TempLabel.Text = $"{ConvertTemp((int)_weather.current.temp)}°C";
    }

	private int ConvertTemp(int temp)
	{
        int temperature = temp - 273;

		return temperature;
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
		OnLocationChanged(LocationEntry.Text);
    }
}

