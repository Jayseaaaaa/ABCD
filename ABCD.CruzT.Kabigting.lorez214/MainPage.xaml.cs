using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Timers;

﻿namespace ABCD.CruzT.Kabigting.lorez214

public partial class MainPage : ContentPage
{
    private Timer _timer;
    private int _seconds = 0;
    private bool _isRunning = false;

    private Location _lastLocation;
    private double _distance = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSearchPressed(object sender, EventArgs e)
    {
        var locations = await Geocoding.GetLocationsAsync(SearchBar.Text);
        var location = locations?.FirstOrDefault();

        if (location != null)
        {
            MainMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Location(location.Latitude, location.Longitude),
                    Distance.FromKilometers(1)));
        }
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            _timer?.Stop();
            _isRunning = false;
            return;
        }

        _timer = new Timer(1000);
        _timer.Elapsed += UpdateTime;
        _timer.Start();

        StartTracking();
        _isRunning = true;
    }

    private void UpdateTime(object sender, ElapsedEventArgs e)
    {
        _seconds++;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            TimeLabel.Text = TimeSpan.FromSeconds(_seconds).ToString(@"mm\:ss");
        });
    }

    private async void StartTracking()
    {
        while (_isRunning)
        {
            var location = await Geolocation.GetLocationAsync();

            if (location != null)
            {
                if (_lastLocation != null)
                {
                    var d = Location.CalculateDistance(_lastLocation, location, DistanceUnits.Kilometers);
                    _distance += d;

                    double speed = location.Speed ?? 0;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        DistanceLabel.Text = _distance.ToString("0.00");
                        SpeedLabel.Text = speed.ToString("0.0");
                    });
                }

                _lastLocation = location;
            }

            await Task.Delay(2000);
        }
    }

    private void OnHistoryClicked(object sender, EventArgs e)
    {
        DisplayAlert("History", "Show saved routes here.", "OK");
    }

    private void OnHelpClicked(object sender, EventArgs e)
    {
        DisplayAlert("Help", "Instructions go here.", "OK");
    }
}
