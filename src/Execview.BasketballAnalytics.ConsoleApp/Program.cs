using System;
using System.IO;
using Execview.BasketballAnalytics.Core;
using Execview.BasketballAnalytics.Core.Data;
using Execview.BasketballAnalytics.Core.Reporting;
using Execview.BasketballAnalytics.Core.Reporting.Enrichers;
using Microsoft.Extensions.DependencyInjection;

namespace Execview.BasketballAnalytics.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dataFile = Path.Combine(AppContext.BaseDirectory, "Data", "chicago-bulls.csv");
            var reportsFolder = Path.Combine(AppContext.BaseDirectory, "Reports");
            var reportFile = Path.Combine(reportsFolder, $"chicago-bulls-stats-{DateTime.Now:yyyy-MM-dd hh-mm-ss}.csv");
            var provider = ConfigureContainer();
            var reporter = provider.GetRequiredService<TeamStatisticsJsonReporter>();

            Console.WriteLine("Hello, please press any key to create stats report about Chicago Bulls team ...");
            Console.ReadKey();

            var reportContents =
                reporter.GetReportAsJsonAsync(dataFile).Result; // TODO: target C# 7.1 which supports async MAIN method

            if (Directory.Exists("reportsFolder") == false)
                Directory.CreateDirectory(reportsFolder);

            File.WriteAllText(reportFile, reportContents);

            Console.WriteLine($"Report has been generated and is located at: {reportFile}");
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        private static IServiceProvider ConfigureContainer()
        {
            var services = new ServiceCollection();

            services.AddTransient<IPlayerParser, PlayerParser>();
            services.AddTransient<IFileDataProvider, CsvDataProvider>();
            services.AddTransient<IImperialToMetricConverter, ImperialToMetricConverter>();
            services.AddTransient<IReportEnricher, AveragePlayerHeightEnricher>();
            services.AddTransient<IReportEnricher, AveragePointsForTeamEnricher>();
            services.AddTransient<IReportEnricher, TeamEnricher>();
            services.AddTransient<IReportEnricher, TopAchieversEnricher>();
            services.AddTransient<ITeamStatisticsReporter, TeamStatisticsReporter>();
            services.AddTransient<TeamStatisticsJsonReporter>();

            return services.BuildServiceProvider();
        }
    }
}