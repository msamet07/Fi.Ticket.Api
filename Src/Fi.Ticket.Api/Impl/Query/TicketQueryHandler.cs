using AutoMapper;
using Fi.Infra.Abstraction;
using Fi.Infra.Context;
using Fi.Infra.Exceptions;
using Fi.Mediator.Interfaces;
using Fi.Persistence.Relational.Interfaces;
using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Api.Domain.Entity;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fi.Ticket.Api.Impl.Query
{
    public class TicketQueryHandler:
     
        IFiRequestHandler<GetTicketByCodeQuery, List<TicketOutputModel>>,
        IFiRequestHandler<GetTicketByKeyQuery, TicketOutputModel>,
        IFiRequestHandler<GetAllTicketQuery, List<TicketOutputModel>>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketQueryHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<List<TicketOutputModel>> Handle(GetTicketByCodeQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var list = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>().Where(x => x.Code == request.Code)
                                                    .OrderBy(x => x.Name)
                                                    .ToListAsNoTrackingAsync(sessionDI.MessageContext);

            return mapper.Map<List<TicketOutputModel>>(list);
        }

        public async Task<TicketOutputModel> Handle(GetTicketByKeyQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var result = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>()
                                        .FirstOrDefaultAsNoTrackingAsync(x => x.Id == request.Id, cancellationToken);
            if (result == null)
                throw exceptionFactory.BadRequestEx(ErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "Ticket"], request.Id);

            return mapper.Map<TicketOutputModel>(result);
        }

        public async Task<List<TicketOutputModel>> Handle(GetAllTicketQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var list = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>()
                                      .OrderBy(x => x.Name)
                                      .ToListAsNoTrackingAsync(sessionDI.MessageContext);

            return mapper.Map<List<TicketOutputModel>>(list);
        }
    }
}
