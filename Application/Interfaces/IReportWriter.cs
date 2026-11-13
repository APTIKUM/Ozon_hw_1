using Domain.Reports;

namespace Application.Interfaces;

public interface IReportWriter
{
    void Write(ClientsReport report);
}