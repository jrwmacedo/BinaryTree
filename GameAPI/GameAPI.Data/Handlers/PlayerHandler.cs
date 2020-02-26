using AutoMapper;
using GameAPI.Data.Context;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Data.Response.PlayerResponse;
using GameAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameAPI.Data.Handlers
{
    public class PlayerHandler : BaseHandler,
        IRequestHandler<CreatePlayer, Player>,
        IRequestHandler<QueryPlayerRequest, IQueryable<PlayerResponse>>,
        IRequestHandler<QuerySinglePlayerRequest, IQueryable<PlayerResponse>>
    {

        public PlayerHandler(DataContext ctx, IMapper mapper) : base(ctx, mapper) { }

        public async Task<Player> Handle(CreatePlayer request, CancellationToken cancellationToken)
        {
            Player player = base.mapper.Map<CreatePlayer, Player>(request);

            base.ctx.Add(player);

            await this.ctx.SaveChangesAsync();

            return player;
        }

        public async Task<IQueryable<PlayerResponse>> Handle(QueryPlayerRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.mapper.ProjectTo<PlayerResponse>(ctx.Players));
        }

        public async Task<IQueryable<PlayerResponse>> Handle(QuerySinglePlayerRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.mapper.ProjectTo<PlayerResponse>(ctx.Players.Include(h => h.PlayerQuestions).Where(v => v.Id == request.key)));
        }
    }
}
