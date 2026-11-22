using Domain.Reports;
using Infrastructure.ReportFormatters;
using FluentAssertions;

namespace Tests;

public sealed class BaseReportFormatterTests
{
    [Test]
    public void Format_IncludesSummaryData()
    {
        var report = new ClientsReport(
            [
                new ClientSummary(10, "Item A", 450m)
            ],
            new DateTime(2026, 11, 22, 12, 30, 0));

        var result = new BaseReportFormatter().Format(report);

        result.Should().Contain("10");
        result.Should().Contain("Item A");
        result.Should().Contain("2026.11.22 12:30:00");
    }
}
