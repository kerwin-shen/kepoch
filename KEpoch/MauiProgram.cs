using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace KEpoch
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                 {
#if WINDOWS
                    events.AddWindows(windows => windows.OnWindowCreated(window =>
                    {
                        window.AppWindow.Resize(new Windows.Graphics.SizeInt32(800, 1000));
                    }));
#endif
                 });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
