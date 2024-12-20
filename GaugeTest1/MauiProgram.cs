﻿using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
//using MicroGauge.Maui;

namespace GaugeTest1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder ();
            builder
                .UseSkiaSharp ()
                .UseMicrocharts()
                
                .UseMauiApp<App> ()
                .ConfigureFonts (fonts =>
                {
                    fonts.AddFont ("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont ("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build ();
        }
    }
}
