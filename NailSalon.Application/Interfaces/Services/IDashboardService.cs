using NailSalon.Application.DTOs.Dashboard;

namespace NailSalon.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}