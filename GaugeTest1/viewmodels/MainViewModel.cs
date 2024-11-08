﻿
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;

using LiveChartsCore.SkiaSharpView;
using Microcharts;
using Microcharts.Maui;
using LiveChartsCore.SkiaSharpView.Maui;
    using System.Windows.Markup;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Microsoft.Maui;

namespace GaugeTest1.ViewModel;

public partial class MainViewModel : ObservableObject
{






    private static ObservableCollection<ObservablePoint> xyCoordinates = new ObservableCollection<ObservablePoint> ();






    public ISeries[] ScatterSeries { get; set; } = new ISeries[]
    {
        new ScatterSeries<ObservablePoint>
        {
            Name = "XY Coordinates",
            Values=xyCoordinates,
            //Fill=null,
            GeometrySize=2,
            //LineSmoothness=0,
            Fill = new SolidColorPaint(SKColors.Blue),
            //DataLabelsSize = 20,
            //DataLabelsPaint = new SolidColorPaint(SKColors.Blue),
            //DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
            // DataLabelsFormatter = (point) => "Fuck"+ point.Coordinate.PrimaryValue.ToString("3")+"G",
            Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 30 }

        } };
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
    string gforceText;

    double accelerationX;
    double accelerationY;
    double accelerationZ; 









    public MainViewModel()
    {
        StartAccelerometer ();
        




    }


    public float[] getAccelerationValues()
    {
        //Console.WriteLine ("getAccelerationValues() Called");
        float[] accelerationValues = new float[3];
        accelerationValues[0] = (float)accelerationX; 
        accelerationValues[1] = (float)accelerationY;
        accelerationValues[2] = (float)accelerationZ;
        return accelerationValues; 
    }


    private void StartAccelerometer()
    {
        try
        {
            Accelerometer.ReadingChanged += OnAccelerometerReadingChanged;
            Accelerometer.Start (SensorSpeed.UI);
        }
        catch (FeatureNotSupportedException) { }
        catch (Exception e) { }
    }

    private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        //Console.WriteLine(xyCoordinates.Count);
        //xyCoordinates.Clear();
        xyCoordinates.Add (new ObservablePoint (e.Reading.Acceleration.X, e.Reading.Acceleration.Y));
        if (xyCoordinates.Count >= 10)
        {
            xyCoordinates.RemoveAt(0);
        }
        GforceText = $"X: {e.Reading.Acceleration.X:F2}, Y: {e.Reading.Acceleration.Y:F2}, Z: {e.Reading.Acceleration.Z:F2}";
        accelerationX = e.Reading.Acceleration.X; 
        accelerationY = e.Reading.Acceleration.Y;
        accelerationZ = e.Reading.Acceleration.Z;
    }




    private void StopAccelerometer()
    {
        Accelerometer.Stop ();
        Accelerometer.ReadingChanged -= OnAccelerometerReadingChanged;
    }
} 
