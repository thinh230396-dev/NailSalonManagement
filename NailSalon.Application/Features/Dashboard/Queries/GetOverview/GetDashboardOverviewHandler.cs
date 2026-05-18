using MediatR;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Dashboard.Queries.GetOverview;

public class GetDashboardOverviewHandler
    : IRequestHandler<GetDashboardOverviewQuery, DashboardOverviewDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDashboardOverviewHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardOverviewDto> Handle(
        GetDashboardOverviewQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
        var appointments = await _unitOfWork.Repository<Appointment>().GetAllAsync();
        var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();

        return new DashboardOverviewDto
        {
            TotalCustomers = customers.Count,
            TotalEmployees = employees.Count,
            TotalAppointments = appointments.Count,
            TotalInvoices = invoices.Count,
            TotalRevenue = invoices.Sum(x => x.TotalAmount)
        };
    }
}