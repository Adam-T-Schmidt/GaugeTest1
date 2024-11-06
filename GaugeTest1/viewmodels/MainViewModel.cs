using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
    using System.Windows.Markup;
using LiveChartsCore.SkiaSharpView.Painting.Effects;

namespace GaugeTest1.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private static ObservableCollection<ObservablePoint> xyCoordinates = new ObservableCollection<ObservablePoint> ();


    public ObservableCollection<ISeries> Series { get; set; } = new ObservableCollection<ISeries>
    {
        new LineSeries<ObservablePoint>
        {
            Values=xyCoordinates,
            Fill=null,
            GeometrySize=0,
            LineSmoothness=0,
            Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 30 }

        }
    };

    public Axis[] XAxes { get; set; } = new Axis[]
    {
        new Axis
        {
            Name = "X Axis",
            NamePaint = new SolidColorPaint(SKColors.Black),

            LabelsPaint = new SolidColorPaint(SKColors.Blue),
            TextSize = 10,
            MaxLimit=1.5,
            MinLimit=-1.5,
            SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 2 }
                }
    };
    public Axis[] YAxes { get; set; } = new Axis[]
    {
        new Axis
        {
            Name = "Y Axis",
            NamePaint = new SolidColorPaint(SKColors.Black),

            LabelsPaint = new SolidColorPaint(SKColors.Blue),
            TextSize = 10,
            MaxLimit=1.5,
            MinLimit=-1.5,
            SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 2 }
                }
    };




    [ObservableProperty]
    double accX;
    [ObservableProperty]
    double accY;

    public MainViewModel()
    {
        StartAccelerometer ();
    }

    private void StartAccelerometer()
    {
        try
        {
            Accelerometer.ReadingChanged += OnAccelerometerReadingChanged;
            Accelerometer.Start (SensorSpeed.Default);
        }
        catch (FeatureNotSupportedException) { }
        catch (Exception e) { }
    }

    private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        Console.WriteLine(xyCoordinates.Count);
        //xyCoordinates.Clear ();
        xyCoordinates.Add (new ObservablePoint (e.Reading.Acceleration.X, e.Reading.Acceleration.Y));

        //xyCoordinates[1].Y = e.Reading.Acceleration.Y;
        //xyCoordinates[1].X = e.Reading.Acceleration.X;
        if (xyCoordinates.Count >=4)
        {
            xyCoordinates.RemoveAt (0);

        }



    }

    private void StopAccelerometer()
    {
        Accelerometer.Stop ();
        Accelerometer.ReadingChanged -= OnAccelerometerReadingChanged;
    }


} 
