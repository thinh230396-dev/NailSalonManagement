using MediatR;
using NailSalon.Application.Features.Invoices.DTOs;

namespace NailSalon.Application.Features.Invoices.Queries.GetById;

public class GetInvoiceByIdQuery : IRequest<InvoiceDto>
{
    public Guid Id { get; set; }

    public GetInvoiceByIdQuery(Guid id)
    {
        Id = id;
    }
}