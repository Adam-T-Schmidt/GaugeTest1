using GaugeTest1.ViewModel;
using LiveChartsCore.SkiaSharpView.Extensions;

namespace GaugeTest1
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel mainViewModel { get; set; } = new MainViewModel ();

        public MainPage()
        {
            InitializeComponent ();
            
        }


    }
}



