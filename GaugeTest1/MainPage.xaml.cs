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

namespace GaugeTest1
{
    public partial class MainPage : ContentPage
    {
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

            chartView.Chart = new RadialGaugeChart () { Entries = entries };
            //calls the function that runs whenever the timer elapses called OnTimedEvent()

            _timer.Elapsed += OnTimedEvent;

            //sets the timer to re-start every time it elapses
            _timer.AutoReset = true;

            //enables the timer
            _timer.Enabled = true;

            //sets the ObservableCollection entries equal to a new one that contains placeholder "ChartEntry"s
            entries = new ObservableCollection<ChartEntry>
            {
                new ChartEntry(0) { Label = "X", ValueLabel = "", Color = SKColor.Parse("#fa0505") }, //Label = the name of the data set that is shown next to the chart
                new ChartEntry(0) { Label = "Y", ValueLabel = "", Color = SKColor.Parse("#0511fa") }, //ValueLabel = the text under the legend label
                new ChartEntry(0) { Label = "Z", ValueLabel = "", Color = SKColor.Parse("#15fa05") }  //I'm sure you can figure out what Color is lol
            };

            //sets the UI element equal to the recently created ObservableCollection (full of dummy data at this point)
            //If this isn't done, the app will either crash with an InxeXOutOfRangeException or the gauge will not show up
            chartView.Chart = new RadialGaugeChart { Entries = entries }; 
        }

        //Every time the timer elapses (100ms pass), do the following:
        private void OnTimedEvent(object sender, ElapsedEventArgs t)
        {
            //create an array of floats to store accelerometer data, then call the function in MainViewModel to access the data
            float[] accValues = mainViewModel.getAccelerationValues ();

            //set each of the EXISTING ChartEntry locations in the ObservableCollection "entries" equal to their respective XYZ acceleration values
            entries[0] = new ChartEntry (accValues[0]) { Label = "X", ValueLabel = accValues[0].ToString(), Color = SKColor.Parse ("#fa0505") };
            entries[1] = new ChartEntry (accValues[1]) { Label = "Y", ValueLabel = accValues[1].ToString (), Color = SKColor.Parse ("#0511fa") };
            entries[2] = new ChartEntry (accValues[2]) { Label = "Z", ValueLabel = accValues[2].ToString (), Color = SKColor.Parse ("#15fa05") };

            /* Set the chart equal to a new RadialGaugeChart containing the new information
             * Animations are disabled because this solution re-draws the graph every time.
             * Microcharts are not designed to update this fast, so they include animations that are only designed to draw once.
             * They look good, but for fast re-draws they make the data unreadable.*/
            chartView.Chart = new RadialGaugeChart () { Entries = entries, AnimationDuration=new TimeSpan(0), IsAnimated=false, MaxValue=2, MinValue=-2};
        }
        //honestly useless but i'm keeping it here anyway. Sue me
        protected override void OnDisappearing()
        {
            base.OnDisappearing ();
            _timer.Stop ();
        }
    }
}



