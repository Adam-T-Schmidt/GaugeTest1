using GaugeTest1.ViewModel;
using LiveChartsCore.SkiaSharpView.Extensions;
using Microcharts;
//using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Timers; 

namespace GaugeTest1
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer _timer = new(100);
        //private IDispatcherTimer _timer;
        public MainViewModel mainViewModel = new MainViewModel ();
        float accelerationX;
        float accelerationY;
        float accelerationZ;
        public ObservableCollection<ChartEntry> entries;

        public MainPage()
        {
            InitializeComponent ();
            chartView.Chart = new RadialGaugeChart () { Entries = entries };
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            entries = new ObservableCollection<ChartEntry>
            {
                new ChartEntry(0) { Label = "X", ValueLabel = "", Color = SKColor.Parse("#fa0505") },
                new ChartEntry(0) { Label = "Y", ValueLabel = "", Color = SKColor.Parse("#0511fa") },
                new ChartEntry(0) { Label = "Z", ValueLabel = "", Color = SKColor.Parse("#15fa05") }
            };
            chartView.Chart = new RadialGaugeChart { Entries = entries };

        }
        private void OnTimedEvent(object sender, ElapsedEventArgs t)
        {
            float[] accValues = mainViewModel.getAccelerationValues ();
            entries[0] = new ChartEntry (accValues[0]) { Label = "X", ValueLabel = accValues[0].ToString(), Color = SKColor.Parse ("#fa0505") };
            entries[1] = new ChartEntry (accValues[1]) { Label = "Y", ValueLabel = accValues[1].ToString (), Color = SKColor.Parse ("#0511fa") };
            entries[2] = new ChartEntry (accValues[2]) { Label = "Z", ValueLabel = accValues[2].ToString (), Color = SKColor.Parse ("#15fa05") };

            chartView.Chart = new RadialGaugeChart () { Entries = entries, AnimationDuration=new TimeSpan(0), IsAnimated=false, MaxValue=2, MinValue=-2};
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing ();
            _timer.Stop (); // Stop the timer when the page disappears
            
        }
    }
}



