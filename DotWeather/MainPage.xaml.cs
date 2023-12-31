﻿using DotWeather.Models;
using DotWeather.Services;

namespace DotWeather;

public partial class MainPage : ContentPage
{
    private readonly DataService _dataService;
    private UserLocation _location;
    private OpenWeather _weather;

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

        _location = await _dataService.GetLocation();
        _weather = await _dataService.GetWeather();

		UpdateDetail();
	}

	private async void OnLocationChanged(string loc)
	{
		_location = await _dataService.GetLocation(loc);
		_weather = await _dataService.GetWeather((double)_location.lat, (double)_location.lon);

		UpdateDetail();
    }

	private int ConvertTemp(int temp)
	{
        int temperature = temp - 273;

		return temperature;
    }

	private void UpdateDetail()
	{
		CityLabel.Text = _location.name;
		WeatherLabel.Text = _weather.current.weather[0].description;
        TempLabel.Text = $"{ConvertTemp((int)_weather.current.temp)}°C";
		PressureLabel.Text = $"{_weather.current.pressure}mbar";
		HumidityLabel.Text = $"{_weather.current.humidity}%";
		WindSpeedLabel.Text = $"{_weather.current.wind_speed}km/h";
		UVILabel.Text = $"{(int)_weather.current.uvi}";
    }

    private async void OnTextChanged(object sender, TextChangedEventArgs e)
    {
		List<UserLocation> locationList = new List<UserLocation>();

		locationList = await _dataService.GetAnyLocation(SearchBar.Text);

		SearchResult.ItemsSource = locationList.Select(l => l.name).ToArray();
    }

    private void OnLocationSelected(object sender, SelectedItemChangedEventArgs e)
    {
		OnLocationChanged(SearchResult.SelectedItem.ToString());

		SearchBar.Text = string.Empty;
		SearchBar.Unfocus();
    }
}

