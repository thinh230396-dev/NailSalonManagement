using NailSalon.Application.DTOs.Dashboard;
using NailSalon.Application.Interfaces;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;

namespace NailSalon.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var now = DateTime.Now;
        var summary = new DashboardSummaryDto();

        // 1. Lấy dữ liệu (Trong thực tế dự án lớn sẽ dùng IQueryable để tối ưu DB, 
        // nhưng ở mức đồ án ta có thể dùng GetAll rồi dùng LINQ xử lý in-memory)
        var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
        var customers = await _unitOfWork.Customers.GetAllAsync();
        var appointments = await _unitOfWork.Appointments.GetAllAsync();

        // 2. Tính doanh thu tháng hiện tại
        summary.TotalRevenueThisMonth = invoices
            .Where(i => i.CreatedAt.Month == now.Month && i.CreatedAt.Year == now.Year)
            .Sum(i => i.TotalAmount);

        // 3. Đếm số khách hàng mới đăng ký trong tháng này
        summary.NewCustomersThisMonth = customers
            .Count(c => c.CreatedAt.Month == now.Month && c.CreatedAt.Year == now.Year);

        // 4. Đếm số lịch hẹn ngày hôm nay
        summary.AppointmentsToday = appointments
            .Count(a => a.AppointmentTime.Date == now.Date);

        // 5. Chuẩn bị dữ liệu cho biểu đồ 12 tháng của năm hiện tại
        for (int i = 1; i <= 12; i++)
        {
            var monthlySum = invoices
                .Where(inv => inv.CreatedAt.Year == now.Year && inv.CreatedAt.Month == i)
                .Sum(inv => inv.TotalAmount);

            summary.RevenueChartData.Add(new MonthlyRevenueDto
            {
                Month = i,
                Revenue = monthlySum
            });
        }

        return summary;
    }
}