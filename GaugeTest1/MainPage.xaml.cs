using GaugeTest1.ViewModel;
using LiveChartsCore.SkiaSharpView.Extensions;
using Microcharts;
using SkiaSharp;

namespace GaugeTest1
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel mainViewModel { get; set; } = new MainViewModel ();
        
        public MainPage()
        {
            InitializeComponent ();
            chartView.Chart = new RadialGaugeChart () { Entries = MainViewModel.entries };


        }
        


    }
}



