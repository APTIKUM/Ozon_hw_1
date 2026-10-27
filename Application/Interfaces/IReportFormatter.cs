using Domain.Reports;

namespace Application.Interfaces;

public interface IReportFormatter
{
    public string Format(ClientsReport report);
}