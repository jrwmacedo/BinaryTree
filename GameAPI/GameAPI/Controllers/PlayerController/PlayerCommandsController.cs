using System;
using System.Threading.Tasks;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.PlayerController
{
    [Route("api/Players")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PlayerCommandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlayerCommandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Player>> Create([FromBody]CreatePlayer request)
        {

            var player = await mediator.Send(request);
            return Created(Request.Path.ToUriComponent(), player);

        }
    }
}