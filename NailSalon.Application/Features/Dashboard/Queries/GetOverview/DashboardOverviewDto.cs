namespace NailSalon.Application.Features.Dashboard.Queries.GetOverview;

public class DashboardOverviewDto
{
    public int TotalCustomers { get; set; }

    public int TotalEmployees { get; set; }

    public int TotalAppointments { get; set; }

    public int TotalInvoices { get; set; }

    public decimal TotalRevenue { get; set; }
}