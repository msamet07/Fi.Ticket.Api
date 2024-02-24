using AutoMapper;
using Fi.Infra.Abstraction;
using Fi.Infra.Context;
using Fi.Infra.Exceptions;
using Fi.Mediator.Message;
using Fi.Persistence.Relational.Interfaces;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using System.Threading.Tasks;
using System.Threading;
using Fi.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;
using Fi.Ticket.Api.Cqrs;

namespace Fi.Ticket.Api.Impl.Command
{
    public class TicketResponseCommandHandler :


        IFiRequestHandler<CreateTicketResponseCommand, TicketResponseOutputModel>,
        IFiRequestHandler<UpdateTicketResponseCommand, TicketResponseOutputModel>,
        IFiRequestHandler<DeleteTicketResponseCommand, VoidResult>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketResponseCommandHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<TicketResponseOutputModel> Handle(CreateTicketResponseCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var entity = mapper.Map<Fi.Ticket.Api.Domain.Entity.TicketResponse>(request.Model);

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TicketResponseOutputModel>(entity);
        }

        public async Task<TicketResponseOutputModel> Handle(UpdateTicketResponseCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            request.Model.Id = request.Id;
            var mapped = mapper.Map<Fi.Ticket.Api.Domain.Entity.TicketResponse>(request.Model);

            var fromDb = await dbContext.Set<Domain.Entity.TicketResponse>().FirstOrDefaultAsync(x => x.Id == request.Model.Id, cancellationToken);
            if (fromDb == null)
                throw exceptionFactory.BadRequestEx(BaseErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "TicketResponse"], request.Model.Id);

            var existedTicketResponse = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.TicketResponse>().FirstOrDefaultAsNoTrackingAsync(x => x.Id != fromDb.Id, cancellationToken);
            if (existedTicketResponse != null)
                throw exceptionFactory.BadRequestEx(ErrorCodes.TicketResponseAlreadyExists, localizer[FiLocalizedStringType.EntityName, "TicketResponse"], existedTicketResponse.Id);

            await dbContext.UpdatePartial(fromDb, mapped);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TicketResponseOutputModel>(fromDb);
        }

        public async Task<VoidResult> Handle(DeleteTicketResponseCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var fromDb = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.TicketResponse>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (fromDb == null)
                throw exceptionFactory.BadRequestEx(BaseErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "TicketResponse"], request.Id);

            dbContext.Remove(fromDb);
            await dbContext.SaveChangesAsync();

            return new VoidResult();
        }
    }
}
