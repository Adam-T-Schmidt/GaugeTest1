/*
 * This is the main view model for this simple acceleration graphing application. 
 * This file collects acceleration data and does the following things with it: 
 * 
 * - Prepares the data into an appropriate datatype for displaying in a livechart using data binding
 * - Provides a function for the main page to gain access to the acceleration data. 
 * 
 * Please see the following comments for details on how it works
 * 
 */
using CommunityToolkit.Mvvm.ComponentModel; //declare libraries in use
using LiveChartsCore;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;


namespace GaugeTest1.ViewModel;

public partial class MainViewModel : ObservableObject
{
    //creates "overarching data type" for storing the XY coordinates of acceleration data
    //this datatype is an observable collection of observable points. Essentially this means it's an array of 
    //XY coordinates that trigger data binding
    private static ObservableCollection<ObservablePoint> xyCoordinates = new ObservableCollection<ObservablePoint> (); 

    //Observable property that updates the acceleration lable on the main page
    [ObservableProperty]
    string gforceText;


    //creates doubles that store acceleration data whenever the reading changes 
    double accelerationX;
    double accelerationY;
    double accelerationZ;


    //creates what is essentially an array of arrays of points that populate the acceleration Livechart (wtf C#?)
    public ISeries[] ScatterSeries { get; set; } = new ISeries[]
    {
        new ScatterSeries<ObservablePoint>
        {
            Name = "XY Coordinates",
            Values=xyCoordinates,
            GeometrySize=2,
            Fill = new SolidColorPaint(SKColors.Blue),
            Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 30 }

        } };

    //Creates custom Axes for the livecharts graph
    //These are data bound to the livecharts graph 
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

    //Start the accelerometer
    public MainViewModel()
    {
        StartAccelerometer ();
    }

    //function to allow the main page to access acceleration data.
    //returns an array[3] of floats that contain X, Y, Z acceleration data in that order
    public float[] getAccelerationValues()
    {
        float[] accelerationValues = new float[3];
        accelerationValues[0] = (float)accelerationX; 
        accelerationValues[1] = (float)accelerationY;
        accelerationValues[2] = (float)accelerationZ;
        return accelerationValues; 
    }

    //the title is pretty self explanatory 
    private void StartAccelerometer()
    {
        try
        {
            Accelerometer.ReadingChanged += OnAccelerometerReadingChanged; // this line triggers the OnAccelerometerReadingChanged() function
            Accelerometer.Start (SensorSpeed.UI); 
        }
        catch (FeatureNotSupportedException) { }
        catch (Exception e) { }
    }

    //this function updates the acceleration data and does the following: 
    private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        //creates a new observable point and appends it to the "overarching data type" described above
        xyCoordinates.Add (new ObservablePoint (e.Reading.Acceleration.X, e.Reading.Acceleration.Y)); 
        //This if-statement determines the number of points on the graph, or in other words, the amount of "trail" the graph has
        if (xyCoordinates.Count >= 10) //if the number of ObservablePoints in the "Overarching data type" is >= 10:
        {
            //remove the first element
            xyCoordinates.RemoveAt(0); 
            //an ObservableCollection automatically shifts the rest of the data set down one index to fill the empty space which is handy. 
        }
        //update the label on the mainpage that shows raw acceleration data
        GforceText = $"X: {e.Reading.Acceleration.X:F2}, Y: {e.Reading.Acceleration.Y:F2}, Z: {e.Reading.Acceleration.Z:F2}"; 
        //updates the above double values to contain the most recent acceleration data
        accelerationX = e.Reading.Acceleration.X; 
        accelerationY = e.Reading.Acceleration.Y;
        accelerationZ = e.Reading.Acceleration.Z;
    }

    //function to stop accelerometer data from being read. Not being used in this application but will be useful in others
    private void StopAccelerometer()
    {
        Accelerometer.Stop ();
        Accelerometer.ReadingChanged -= OnAccelerometerReadingChanged;
    }
} 
