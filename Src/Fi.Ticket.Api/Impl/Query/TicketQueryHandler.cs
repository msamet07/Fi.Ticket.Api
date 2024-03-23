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

namespace Fi.Ticket.Api.Impl.Query//burada select sorgu işlemlerimi yapıyorum.Read işlemleri.
{
    public class TicketQueryHandler:
     
        IFiRequestHandler<GetTicketByCodeQuery, List<TicketOutputModel>>,//Burada Handlerımı yazacağım , GetTicketByCodeQuery diye bişey yazacağım bana liste şeklinde bir TicketOutputModel dönecek.
        IFiRequestHandler<GetTicketByKeyQuery, TicketOutputModel>,//IFiRequestHandler da altyapıdan geliyor.
        IFiRequestHandler<GetAllTicketQuery, List<TicketOutputModel>>
    {
        private readonly ISessionContextDI sessionDI;
        private readonly FiTicketDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IExceptionFactory exceptionFactory;
        private readonly IJsonStringLocalizer localizer;

        public TicketQueryHandler(ISessionContextDI sessionDI, IFiModuleDbContext dbContext, IMapper mapper, IExceptionFactory exceptionFactory, IJsonStringLocalizer localizer)//burda bir dependency resolver var.Çeşitli interfaceleri contrakçırdan alıyor.Injection var ama Solid de de Dependency resolver diye geçiyor bu olay.
        {
            this.sessionDI = sessionDI;
            this.dbContext = dbContext as FiTicketDbContext;
            this.mapper = mapper;
            this.exceptionFactory = exceptionFactory;
            this.localizer = localizer;
        }

        public async Task<List<TicketOutputModel>> Handle(GetTicketByCodeQuery request, CancellationToken cancellationToken)//requesti controlerda oluşturdum.
        {
            sessionDI.ExecutionTrace.InitTrace();//Bir trace başlatıyor ama içeriğini tam anlamadım.Diğer yerlerde olduğu için kullandım.

            var list = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>().Where(x => x.Code == request.Code)
                                                    .OrderBy(x => x.Name)
                                                    .ToListAsNoTrackingAsync(sessionDI.MessageContext);//where kriterini girip liste halinde döneceği için tolist diyorum.Bütün ticketlerı bana geriye dön diyorum.

            return mapper.Map<List<TicketOutputModel>>(list);//mapper ile benim listemi verip oluşturup output şeklinde geriye döndürüyorum.
        }

        public async Task<TicketOutputModel> Handle(GetTicketByKeyQuery request, CancellationToken cancellationToken)
        {
            sessionDI.ExecutionTrace.InitTrace();

            var result = await dbContext.Set<Fi.Ticket.Api.Domain.Entity.Ticket>()
                                        .FirstOrDefaultAsNoTrackingAsync(x => x.Id == request.Id, cancellationToken);//ıdsine göre 1 tane sonuç getireceği için firstof
            if (result == null)//exceptionFactory de altyapıdan geliyor ve kontrol için kullanıyorum.Anlamlı bir hata dönebilmek için.
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
