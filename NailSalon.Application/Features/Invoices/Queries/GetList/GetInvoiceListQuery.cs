using MediatR;
using NailSalon.Application.Features.Invoices.DTOs;

namespace NailSalon.Application.Features.Invoices.Queries.GetList;

public class GetInvoiceListQuery : IRequest<List<InvoiceDto>>
{
}