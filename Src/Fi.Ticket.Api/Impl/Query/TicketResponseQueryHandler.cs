using AutoMapper;
using Fi.Infra.Abstraction;
using Fi.Infra.Context;
using Fi.Infra.Exceptions;
using Fi.Persistence.Relational.Interfaces;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Fi.Mediator.Interfaces;
using Fi.Mediator.Message;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Api.Domain.Entity;

namespace Fi.Ticket.Api.Impl.Query
{
    public class TicketResponseQueryHandler:

        IFiRequestHandler<GetTicketResponseByKeyQuery, TicketResponseOutputModel>,
        IFiRequestHandler<GetAllTicketResponseQuery, List<TicketResponseOutputModel>>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketResponseQueryHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<TicketResponseOutputModel> Handle(GetTicketResponseByKeyQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var result = await dbContext.Set<TicketResponse>()
                                        .FirstOrDefaultAsNoTrackingAsync(x => x.Id == request.Id, cancellationToken);
            if (result == null)
                throw exceptionFactory.BadRequestEx(ErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "TicketResponse"], request.Id);

            return mapper.Map<TicketResponseOutputModel>(result);
        }

        public async Task<List<TicketResponseOutputModel>> Handle(GetAllTicketResponseQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var list = await dbContext.Set<TicketResponse>()
                                      .OrderBy(x => x.CreateTime)
                                      .ToListAsNoTrackingAsync(sessionDI.MessageContext);

            return mapper.Map<List<TicketResponseOutputModel>>(list);
        }
    }
}
