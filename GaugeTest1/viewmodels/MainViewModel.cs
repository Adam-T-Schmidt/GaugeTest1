using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;

namespace GaugeTest1.ViewModel;

public partial class MainViewModel : ObservableObject
{

    public ObservableCollection<float> _accelerometerDataX = new ();
    //public ObservableCollection<float> _accelerometerDataY = new();
    //public ObservableCollection<float> _accelerometerDataZ = new();
    //float myCustomValue; 

    //[ObservableProperty]
    //ISeries[] Series;

    [ObservableProperty]
    public IEnumerable<ISeries> series;

    [ObservableProperty]
    public ObservableValue _gaugeItem;

    //GaugeGenerator.BuildSolidGauge(
    //    new GaugeItem(30, series =>
    //    {
    //        series.Fill = new SolidColorPaint(SKColors.YellowGreen);
    //        series.DataLabelsSize = 50;
    //        series.DataLabelsPaint = new SolidColorPaint(SKColors.Red);
    //        series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
    //        series.InnerRadius = 75;
    //    }

    //    ),
    //    new GaugeItem(GaugeItem.Background, series =>
    //    {
    //        series.InnerRadius = 75;
    //        series.Fill = new SolidColorPaint(new SKColor(100, 181, 246, 90));
    //    })); 






    public MainViewModel()
    {




        //Series = new ISeries[]
        //{
        //new ColumnSeries<float>
        //{
        //    Values = _accelerometerDataX
        //}
        ////    //new ColumnSeries<float>
        ////    //{
        ////    //    Values = _accelerometerDataY
        ////    //},
        ////    //new ColumnSeries<float>
        ////    //{
        ////    //    Values = _accelerometerDataZ
        ////    //}
        //};



        StartAccelerometer ();
        InitializeGauge ();
        System.Console.WriteLine ("Graph Initialized");

    }


    public void InitializeGauge()
    {

        _gaugeItem = new ObservableValue (50);
        Series = GaugeGenerator.BuildSolidGauge (
                 new GaugeItem (_gaugeItem, series =>
                 {
                     series.Fill = new SolidColorPaint (SKColors.YellowGreen);
                     series.DataLabelsSize = 50;
                     series.DataLabelsPaint = new SolidColorPaint (SKColors.Red);
                     series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                     series.InnerRadius = 75;
                 }),
                 new GaugeItem (10, series =>
                 {
                     series.InnerRadius = 75;
                     series.Fill = new SolidColorPaint (new SKColor (100, 181, 246, 90));
                 }));
        //System.Console.WriteLine("Graph Initialized Within Function");
        //System.Console.WriteLine("Gauge: " + GaugeItem.Value);
    }




    public void UpdateGagueValue(double newValue)
    {
        //Series = GaugeGenerator.BuildSolidGauge(
        //    new GaugeItem(newValue, series =>
        //    {
        //        series.Fill = new SolidColorPaint(SKColors.YellowGreen);
        //        series.DataLabelsSize = 50;
        //        series.DataLabelsPaint = new SolidColorPaint(SKColors.Red);
        //        series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
        //        series.InnerRadius = 75;
        //    }

        //    ),
        //    new GaugeItem(GaugeItem.Background, series =>
        //    {
        //        series.InnerRadius = 75;
        //        series.Fill = new SolidColorPaint(new SKColor(100, 181, 246, 90));
        //    }));
        _gaugeItem.Value = newValue;



        //System.Console.WriteLine("new Graph Value: "+newValue);
        //System.Console.WriteLine("new Graph Value: " + _gaugeItem.Value);


    }





    private void StartAccelerometer()
    {
        try
        {
            Accelerometer.ReadingChanged += OnAccelerometerReadingChanged;
            Accelerometer.Start (SensorSpeed.UI);
        }
        catch (FeatureNotSupportedException)
        {

        }
        catch (Exception e)
        {

        }


    }

    //AccelerometerLabel.Text = $"X: {reading.Acceleration.X:F2}, Y: {reading.Acceleration.Y:F2}, Z: {reading.Acceleration.Z:F2}";


    private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {

        double accelerationX = e.Reading.Acceleration.X;
        //ObservableValue newGaugeValue = new ObservableValue(accelerationX*10);
        UpdateGagueValue (accelerationX * 10);
        //float accelerationY = e.Reading.Acceleration.Y;
        //float accelerationZ = e.Reading.Acceleration.Z;
        //_accelerometerDataX.Clear ();
        ////_accelerometerDataY.Clear ();
        ////_accelerometerDataZ.Clear ();
        //_accelerometerDataX.Add (accelerationX);
        //Series.
        //_accelerometerDataY.Add (accelerationY);
        //_accelerometerDataZ.Add (accelerationZ);


    }

    private void StopAccelerometer()
    {
        Accelerometer.Stop ();
        Accelerometer.ReadingChanged -= OnAccelerometerReadingChanged;
    }












}
