using GaugeTest1.ViewModel;

namespace GaugeTest1
{


    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource _cts;
        public MainViewModel mainViewModel { get; set; } = new MainViewModel ();

        public MainPage()
        {
            InitializeComponent ();
            //BindingContext = new MainViewModel ();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing ();
            //StartLocationUpdates ();
            await Task.Delay (100);
            mainViewModel.InitializeGauge ();
            await Task.Delay (100);
            InvalidateMeasure ();


        }

        private void StartLocationUpdates()
        {
            _cts = new CancellationTokenSource ();
            //GetLocationAsync(_cts.Token);
        }

        private async Task GetLocationAsync(CancellationToken cancellationToken)
        {
            String BSS = "";
            try
            {
                // Request location updates
                while (!cancellationToken.IsCancellationRequested)
                {
                    var location = await Geolocation.GetLocationAsync (new GeolocationRequest (GeolocationAccuracy.Best, TimeSpan.FromSeconds (5)));

                    if (location != null)
                    {
                        BSS = $"Latitude: {location.Latitude}, \nLongitude: {location.Longitude}";
                    }
                    else
                    {
                        BSS = "Unable to get location.";
                    }
                }
            }
            catch (FeatureNotSupportedException)
            {
                BSS = "Geolocation is not supported.";
            }
            catch (PermissionException)
            {
                BSS = "Permission to access location was denied.";
            }
            catch (Exception ex)
            {
                BSS = $"Unable to get location: {ex.Message}";
            }
        }





        protected override void OnDisappearing()
        {
            base.OnDisappearing ();
            StopLocationUpdates ();

        }

        private void StopLocationUpdates()
        {
            _cts.Cancel ();
        }

        private void click_Clicked(object sender, EventArgs e)
        {

        }
    }
}
