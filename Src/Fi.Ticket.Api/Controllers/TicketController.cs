using Fi.ApiBase.Attribute;
using Fi.ApiBase.Controller;
using Fi.Mediator.Message;
using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Schema.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fi.Ticket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Ticket/[controller]")]
    public class TicketController : ApiControllerBase<TicketController>
    {
        ApiControllerDI<TicketController> baseDI;
        public TicketController(ApiControllerDI<TicketController> baseDI) : base(baseDI)
        {
            this.baseDI = baseDI;
        }

        [ApiKey("20a0b529-d4e4-4557-bae3-726f0a696c2d")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpGet("{Id:int}")]
        public async Task<ApiResponse<TicketOutputModel>> GetByKey(int Id)
        {
            var cmd = new GetTicketByKeyQuery(Id);

            var result = await base.Execute<TicketOutputModel>(cmd);

            return result;
        }

        [ApiKey("f81b0abe-57b1-4166-b1e6-5b6032d1ca28")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResponse<List<TicketOutputModel>>> GetAllList()
        {
            var cmd = new GetAllTicketQuery();

            var result = await base.Execute<List<TicketOutputModel>>(cmd);

            return result;
        }

        [ApiKey("cdb2c879-e465-4e15-982e-650ab50bedc4")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpGet("BySampleCode/{SampleCode:length(1,3)}")]
        public async Task<ApiResponse<List<TicketOutputModel>>> GetByTicketCode(string TicketCode)
        {
            var cmd = new GetTicketByCodeQuery(TicketCode);

            var result = await base.Execute<List<TicketOutputModel>>(cmd);

            return result;
        }

        [ApiKey("336d7131-f153-4caa-895e-9a68c900de97")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpPost]
        [OnlyForDevelopment]
        public async Task<ApiResponse<TicketOutputModel>> Create([FromBody] TicketInputModel model)
        {
            var cmd = new CreateTicketCommand(model);

            var result = await base.Execute<TicketOutputModel>(cmd);

            return result;
        }
        [ApiKey("dc40e3c8-d69c-4eed-bc08-c6b457e3cef7")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpPut("{Id:int}")]
        [OnlyForDevelopment]
        public async Task<ApiResponse<TicketOutputModel>> Update(int Id, [FromBody] TicketInputModel model)
        {
            var cmd = new UpdateTicketCommand(Id, model);

            var result = await base.Execute<TicketOutputModel>(cmd);

            return result;
        }

        [ApiKey("fbb08e30-6ea2-4beb-98e7-065a785ea68a")]
        [ApiAuthorizationAttribute(Infra.Schema.Model.ApiAccessType.Public)]
        [HttpDelete("{Id:int}")]
        [OnlyForDevelopment]
        public async Task<ApiResponse> DeleteByKey(int Id)
        {
            var cmd = new DeleteTicketCommand(Id);

            await base.Execute<VoidResult>(cmd);

            return new ApiResponse();
        }
    }
}
