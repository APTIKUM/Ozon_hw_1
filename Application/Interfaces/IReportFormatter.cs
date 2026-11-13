using Domain.Reports;

namespace Application.Interfaces;

public interface IReportFormatter
{
    string Format(ClientsReport report);
}