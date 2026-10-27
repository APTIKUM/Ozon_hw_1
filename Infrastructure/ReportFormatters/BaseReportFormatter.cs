using System.Text;
using Application.Interfaces;
using Domain.Reports;

namespace Infrastructure.ReportFormatters;

public class BaseReportFormatter : IReportFormatter
{
    public string Format(ClientsReport report)
    {
        var sb = new StringBuilder();

        sb.AppendLine("=======================================================================");
        sb.AppendLine($"                    ОТЧЕТ ПО КЛИЕНТАМ (ОТ {report.CreatedDateTime:yyyy.MM.dd HH:mm:ss})");
        sb.AppendLine("=======================================================================");
        sb.AppendLine($"{"ID Клиента",-15} | {"Сумма заказов",-15} | {"Любимый товар",-30} ");
        sb.AppendLine("-----------------------------------------------------------------------");

        foreach (var summary in report.ClientSummaries)
            sb.AppendLine($"{summary.ClientId,-15} | {summary.OrdersSum,15:N2} | {summary.MostPurchasedItemName,-30}");

        sb.AppendLine("=======================================================================");

        return sb.ToString();
    }
}