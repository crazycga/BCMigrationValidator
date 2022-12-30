using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace ExtractPartitionData;

internal static class Program
{
    public static IConfiguration Config;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Config = builder.Build();

        ApplicationConfiguration.Initialize();
        Application.Run(new mainForm());
    }
}