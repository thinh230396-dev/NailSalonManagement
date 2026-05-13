using NailSalon.Application.DTOs.Dashboard;

namespace NailSalon.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}