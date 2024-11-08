/*
 * Welcome to the main page for this demo application. 
 * This file handles the Microcharts/Gauge version of the acceleration graph
 * Enough said so see the following comments for details on how it works
 */

using GaugeTest1.ViewModel; //define the libraries in use
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Timers;
using MicroGauge.Maui; 

namespace GaugeTest1
{
    public partial class MainPage : ContentPage
    {

        //MauiGaugeRadial RadialGauge = new MauiGaugeRadial
        //{
        //    Value = 15.0,
        //    NeedleBrush = new SolidColorBrush(Colors.Green),
        //    MinValue=-2,
        //    MaxValue=2
        //};
        //creates a timer with an interval of 100 ms to provide a "reason" for the mainpage to access acceleration data
        private System.Timers.Timer _timer = new(100); 

        //creates an instance of MainViewModel to allow the file to call functions contained in MainViewModel
        public MainViewModel mainViewModel = new MainViewModel ();

        //create variables to store acceleration data 
        float accelerationX;
        float accelerationY;
        float accelerationZ;

        //creates an ObservableCollection of "ChartEntry"s that define a few properties of how the chart will look as well as the data itself 
        public ObservableCollection<ChartEntry> entries;

        public MainPage()
        {
            InitializeComponent (); 
            //The UI element that contains the chart needs to be passed a "RadialGaugeChart" that contains "ChartEntry"s 

            //chartView.Chart = new RadialGaugeChart () { Entries = entries };
            //calls the function that runs whenever the timer elapses called OnTimedEvent()

            _timer.Elapsed += OnTimedEvent;

            //sets the timer to re-start every time it elapses
            _timer.AutoReset = true;

            //enables the timer
            _timer.Enabled = true;
           
        }

        //Every time the timer elapses (100ms pass), do the following:
        private void OnTimedEvent(object sender, ElapsedEventArgs t)
        {
            //create an array of floats to store accelerometer data, then call the function in MainViewModel to access the data
            float[] accValues = mainViewModel.getAccelerationValues ();
            //RadialGauge.Value = accValues [0];
            Gauge1.Value = accValues [0];
            Gauge2.Value = accValues[0];
            Gauge3.Value = accValues[1];
            Gauge4.Value = accValues[1];
            Gauge5.Value = accValues[2];
            Gauge6.Value = accValues[2];


        }
        //honestly useless but i'm keeping it here anyway. Sue me
        protected override void OnDisappearing()
        {
            base.OnDisappearing ();
            _timer.Stop ();
        }
    }
}



