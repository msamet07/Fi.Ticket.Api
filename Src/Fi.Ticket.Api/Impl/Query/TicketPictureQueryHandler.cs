using AutoMapper;
using Fi.Infra.Abstraction;
using Fi.Infra.Context;
using Fi.Infra.Exceptions;
using Fi.Mediator.Interfaces;
using Fi.Persistence.Relational.Interfaces;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Api.Domain.Entity;

namespace Fi.Ticket.Api.Impl.Query
{
    public class TicketPictureQueryHandler:
    
        IFiRequestHandler<GetTicketPictureByKeyQuery, TicketPictureOutputModel>,
        IFiRequestHandler<GetAllTicketPictureQuery, List<TicketPictureOutputModel>>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketPictureQueryHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<TicketPictureOutputModel> Handle(GetTicketPictureByKeyQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var result = await dbContext.Set<TicketPicture>()
                                        .FirstOrDefaultAsNoTrackingAsync(x => x.Id == request.Id, cancellationToken);
            if (result == null)
                throw exceptionFactory.BadRequestEx(ErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "TicketPicture"], request.Id);

            return mapper.Map<TicketPictureOutputModel>(result);
        }

        public async Task<List<TicketPictureOutputModel>> Handle(GetAllTicketPictureQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var list = await dbContext.Set<TicketPicture>()
                                      .OrderBy(x => x.PictureId)
                                      .ToListAsNoTrackingAsync(sessionDI.MessageContext);

            return mapper.Map<List<TicketPictureOutputModel>>(list);
        }
    }
}
