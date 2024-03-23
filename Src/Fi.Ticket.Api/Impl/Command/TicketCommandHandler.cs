using AutoMapper;
using Fi.Infra.Abstraction;
using Fi.Infra.Context;
using Fi.Infra.Exceptions;
using Fi.Mediator.Interfaces;
using Fi.Mediator.Message;
using Fi.Persistence.Relational.Interfaces;
using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Api.Domain.Entity;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Fi.Ticket.Api.Impl.Command //oluşturma silme güncelleme işlemleri için oluşturuyorum.
{
    public class TicketCommandHandler :

        IFiRequestHandler<CreateTicketCommand, TicketOutputModel>,
        IFiRequestHandler<UpdateTicketCommand, TicketOutputModel>,
        IFiRequestHandler<DeleteTicketCommand, VoidResult>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketCommandHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<TicketOutputModel> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var existedTicket = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>().FirstOrDefaultAsNoTrackingAsync(x => x.Code == request.Model.Code, cancellationToken);
            if (existedTicket != null)//aynı koddan varmı kontrolü yapıyoruz.
                throw exceptionFactory.BadRequestEx(ErrorCodes.TicketAlreadyExists, localizer[FiLocalizedStringType.EntityName, "Ticket"], existedTicket.Id);

            var entity = mapper.Map<Fi.Ticket.Api.Domain.Entity.Ticket>(request.Model);
            //Ticket input model geldiği için bunu mapper ile ticketa çeviriyoruz
            await dbContext.AddAsync(entity);//veritabanına gönderiyoruz ama tek başına yeterli değil
            await dbContext.SaveChangesAsync();// o yüzden bunuda ekliyoruz.
            return mapper.Map<TicketOutputModel>(entity);//entity frameworkün özelliği olarak buraya kadar bir ıd yoktu,ıd oluştur ve buradada ıd li halini mapleyerek döndü.
        }

        public async Task<TicketOutputModel> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            request.Model.Id = request.Id;
            var mapped = mapper.Map<Fi.Ticket.Api.Domain.Entity.Ticket>(request.Model);

            var fromDb = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>().FirstOrDefaultAsync(x => x.Id == request.Model.Id, cancellationToken);
            if (fromDb == null)
                throw exceptionFactory.BadRequestEx(BaseErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "Ticket"], request.Model.Id);

            var existedTicket = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>().FirstOrDefaultAsNoTrackingAsync(x => x.Id != fromDb.Id && x.Code == request.Model.Code, cancellationToken);
            if (existedTicket != null)
                throw exceptionFactory.BadRequestEx(ErrorCodes.TicketAlreadyExists, localizer[FiLocalizedStringType.EntityName, "Ticket"], existedTicket.Id);

            await dbContext.UpdatePartial(fromDb, mapped);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TicketOutputModel>(fromDb);
        }

        public async Task<VoidResult> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var fromDb = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (fromDb == null)
                throw exceptionFactory.BadRequestEx(BaseErrorCodes.ItemDoNotExists, localizer[FiLocalizedStringType.EntityName, "Ticket"], request.Id);

            dbContext.Remove(fromDb);
            await dbContext.SaveChangesAsync();

            return new VoidResult();//bişey dönmeyeceksem voidresult diyorum.
        }
    }
}
//buradan validatörlere geçelim.