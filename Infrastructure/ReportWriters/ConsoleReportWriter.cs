using Application.Interfaces;
using Domain.Reports;

namespace Infrastructure.ReportWriters;

public class ConsoleReportWriter(IReportFormatter formatter) : IReportWriter
{
    private readonly IReportFormatter _formatter = formatter;

    public void Write(ClientsReport report)
    {
        Console.WriteLine(formatter.Format(report));
    }
}