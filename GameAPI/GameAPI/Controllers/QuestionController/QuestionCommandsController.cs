using System.Threading.Tasks;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.QuestionController
{
    [Route("api/Questions")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuestionCommandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuestionCommandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlayerQuestion>> Create([FromBody]CreatePlayerQuestion request)
        {
            var playerQuestion = await mediator.Send(request);

            return Created(Request.Path.ToUriComponent(), playerQuestion);
        }
    }
}