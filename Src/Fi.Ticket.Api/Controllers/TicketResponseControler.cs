using Fi.ApiBase.Attribute;
using Fi.ApiBase.Controller;
using Fi.Mediator.Message;
using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Schema.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fi.Ticket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Ticket/[controller]")]
    public class TicketResponseControler : ApiControllerBase<TicketPictureController>
    {

        public TicketResponseControler(ApiControllerDI<TicketPictureController> baseDI) : base(baseDI)
        {
        }

        [ApiKey("20a0b529-d4e4-4557-bae3-726f0a696c2d")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpGet("{Id:int}")]
        public async Task<ApiResponse<TicketResponseOutputModel>> GetByKey(int Id)
        {
            var cmd = new GetTicketResponseByKeyQuery(Id);

            var result = await base.Execute<TicketResponseOutputModel>(cmd);

            return result;
        }

        [ApiKey("f81b0abe-57b1-4166-b1e6-5b6032d1ca28")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpGet]
        public async Task<ApiResponse<List<TicketResponseOutputModel>>> GetAllList()
        {
            var cmd = new GetAllTicketResponseQuery();

            var result = await base.Execute<List<TicketResponseOutputModel>>(cmd);

            return result;
        }

        [ApiKey("336d7131-f153-4caa-895e-9a68c900de97")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpPost]
        [OnlyForDevelopment]
        public async Task<ApiResponse<TicketResponseOutputModel>> Create([FromBody] TicketResponseInputModel model)
        {
            var cmd = new CreateTicketResponseCommand(model);

            var result = await base.Execute<TicketResponseOutputModel>(cmd);

            return result;
        }
        [ApiKey("dc40e3c8-d69c-4eed-bc08-c6b457e3cef7")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpPut("{Id:int}")]
        [OnlyForDevelopment]
        public async Task<ApiResponse<TicketResponseOutputModel>> Update(int Id, [FromBody] TicketResponseInputModel model)
        {
            var cmd = new UpdateTicketResponseCommand(Id, model);

            var result = await base.Execute<TicketResponseOutputModel>(cmd);

            return result;
        }

        [ApiKey("fbb08e30-6ea2-4beb-98e7-065a785ea68a")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpDelete("{Id:int}")]
        [OnlyForDevelopment]
        public async Task<ApiResponse> DeleteByKey(int Id)
        {
            var cmd = new DeleteTicketResponseCommand(Id);

            await base.Execute<VoidResult>(cmd);

            return new ApiResponse();
        }

    }
}
