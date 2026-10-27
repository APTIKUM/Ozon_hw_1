using Application.Interfaces;
using Domain.Reports;

namespace Infrastructure.ReportWriters;

public class FileReportWriter(IReportFormatter formatter) : IReportWriter
{
    private readonly IReportFormatter _formatter = formatter;

    public void Write(ClientsReport report)
    {
        var fileName = $"Clients_Report_{report.CreatedDateTime:dd_MM_yyyy_HH_mm_ss}.txt";

        using var writer = new StreamWriter(fileName);

        writer.WriteLine(_formatter.Format(report));
    }
}