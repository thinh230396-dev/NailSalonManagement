using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Features.Dashboard.Queries.GetOverview;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
    {
        var result = await _mediator.Send(new GetDashboardOverviewQuery());
        return Ok(result);
    }
}