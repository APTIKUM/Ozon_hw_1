using Application.Interfaces;
using Application.Services;
using Infrastructure.ReportFormatters;
using Infrastructure.ReportWriters;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<ReportService>();
services.AddSingleton<IOrderRepository, CsvOrderRepository>();
services.AddSingleton<IReportFormatter, BaseReportFormatter>();

if (args.Length == 0)
{
    Console.Error.WriteLine("Specify writer: console or file");
    return 1;
}

switch (args[0])
{
    case "console":
        services.AddTransient<IReportWriter, ConsoleReportWriter>();
        break;
    case "file":
        services.AddTransient<IReportWriter, FileReportWriter>();
        break;
    default:
        Console.Error.WriteLine("Writer must be 'console' or 'file'");
        return 1;
}

var provider = services.BuildServiceProvider();

var reportService = provider.GetRequiredService<ReportService>();

try
{
    var clientsIds = args
        .Skip(1)
        .Select(long.Parse)
        .ToList();

    var report = reportService.GenerateClientsReport(clientsIds);

    reportService.WriteClientReport(report);

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}