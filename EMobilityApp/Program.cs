using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMobilityApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("Starting: {0}", Application.ProductName);
            Log.Information(".NET Version: {0}", Environment.Version.ToString());

            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());

                Log.Information("End of: {0}", Application.ProductName);
                Log.Information("***************************************************  -- END --  ****************************************************************");
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Abnormal error while running the application");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
