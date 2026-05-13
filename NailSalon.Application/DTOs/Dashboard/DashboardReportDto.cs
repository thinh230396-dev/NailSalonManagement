namespace NailSalon.Application.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public decimal TotalRevenueThisMonth { get; set; }
    public int NewCustomersThisMonth { get; set; }
    public int AppointmentsToday { get; set; }

    // Dùng để vẽ biểu đồ cột doanh thu 12 tháng
    public List<MonthlyRevenueDto> RevenueChartData { get; set; } = new List<MonthlyRevenueDto>();
}