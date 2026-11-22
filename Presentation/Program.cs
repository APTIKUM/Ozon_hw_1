using Application.Interfaces;
using Application.Services;
using Infrastructure.ReportFormatters;
using Infrastructure.ReportWriters;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Presentation;

var services = new ServiceCollection();

services.AddSingleton<ReportService>();
services.AddSingleton<IOrderRepository, CsvOrderRepository>();
services.AddSingleton<IReportFormatter, BaseReportFormatter>();

if (!ArgumentParser.TryParse(args, out var parsedArguments, out var parsingError))
{
    Console.Error.WriteLine(parsingError);
    return 1;
}

switch (parsedArguments?.Writer)
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
    var report = reportService.GenerateClientsReport(parsedArguments.ClientIds);

    reportService.WriteClientReport(report);

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}