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
    public class TicketPictureCommandHandler :


        IFiRequestHandler<CreateTicketPictureCommand, TicketPictureOutputModel>,
        IFiRequestHandler<UpdateTicketPictureCommand, TicketPictureOutputModel>,
        IFiRequestHandler<DeleteTicketPictureCommand, VoidResult>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketPictureCommandHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<TicketPictureOutputModel> Handle(CreateTicketPictureCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var entity = mapper.Map<Fi.Ticket.Api.Domain.Entity.TicketPicture>(request.Model);

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TicketPictureOutputModel>(entity);
        }

        public async Task<TicketPictureOutputModel> Handle(UpdateTicketPictureCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            request.Model.Id = request.Id;
            var mapped = mapper.Map<Fi.Ticket.Api.Domain.Entity.TicketPicture>(request.Model);

            var fromDb = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.TicketPicture>().FirstOrDefaultAsync(x => x.Id == request.Model.Id, cancellationToken);
            if (fromDb == null)
                throw exceptionFactory.BadRequestEx(BaseErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "TicketPicture"], request.Model.Id);

            var existedTicketPicture = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.TicketPicture>().FirstOrDefaultAsNoTrackingAsync(x => x.Id != fromDb.Id, cancellationToken);
            if (existedTicketPicture != null)
                throw exceptionFactory.BadRequestEx(ErrorCodes.TicketPictureAlreadyExists, localizer[FiLocalizedStringType.EntityName, "TicketPicture"], existedTicketPicture.Id);

            await dbContext.UpdatePartial(fromDb, mapped);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TicketPictureOutputModel>(fromDb);
        }

        public async Task<VoidResult> Handle(DeleteTicketPictureCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var fromDb = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.TicketPicture>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (fromDb == null)
                throw exceptionFactory.BadRequestEx(BaseErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "TicketPicture"], request.Id);

            dbContext.Remove(fromDb);
            await dbContext.SaveChangesAsync();

            return new VoidResult();
        }
    }
}
