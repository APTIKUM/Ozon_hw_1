using Domain.Reports;

namespace Application.Interfaces;

public interface IReportWriter
{
    public void Write(ClientsReport report);
}