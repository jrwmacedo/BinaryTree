using System.Linq;
using System.Threading.Tasks;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Data.Response.PlayerResponse;
using GameAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers.PlayerController
{
    public class PlayersController : ODataController
    {
        private readonly IMediator mediator;

        public PlayersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [EnableQuery]
        public async Task<SingleResult<PlayerResponse>> GetPlayer(QuerySinglePlayerRequest request)
        {
            var player = await mediator.Send(request);
            return new SingleResult<PlayerResponse>(player);
        }

        [EnableQuery]
        public async Task<IQueryable<PlayerResponse>> GetPlayers(QueryPlayerRequest request)
        {
            return await mediator.Send(request);
        }
    }
}