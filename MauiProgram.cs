using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Assignment9.DB_Services;

namespace Assignment9;

public static class MauiProgram
{


    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Define the database path
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "assignment9.db");

        // Add logging to confirm dbPath
        Console.WriteLine($"Database path in MauiProgram: {dbPath}");

        // Register DatabaseServices as a singleton
        builder.Services.AddSingleton<DatabaseServices>(s => new DatabaseServices(dbPath));

        // Register the App with the dbPath parameter
        builder.Services.AddSingleton<App>(s => new App(dbPath));

        // Use a function to resolve App instead of UseMauiApp<App>()
        builder.UseMauiApp(s => s.GetRequiredService<App>());

        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
