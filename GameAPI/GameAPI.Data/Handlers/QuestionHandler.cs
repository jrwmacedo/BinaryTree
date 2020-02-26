using AutoMapper;
using GameAPI.Data.Context;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Data.Request.QuestionRequest;
using GameAPI.Data.Response.QuestionResponse;
using GameAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameAPI.Data.Handlers
{
    public class QuestionHandler : BaseHandler, 
        IRequestHandler<CreatePlayerQuestion, IList<PlayerQuestion>>, 
        IRequestHandler<QueryQuestionRequest, IQueryable<QuestionResponse>>,
         IRequestHandler<QuerySingleQuestionRequest, IQueryable<QuestionResponse>>
    {
        public QuestionHandler(DataContext ctx, IMapper mapper) : base(ctx, mapper) { }

        public async Task<IList<PlayerQuestion>> Handle(CreatePlayerQuestion request, CancellationToken cancellationToken)
        {
            Player player = await base.ctx.Players.SingleOrDefaultAsync(v => v.Id == request.Id);
            IList<Question> questions = await base.ctx.Questions.Where(q => request.Answers.Contains(q.Id)).ToListAsync();
            player.PlayerQuestions = new List<PlayerQuestion>();
            foreach (var question in questions)
            {
                player.PlayerQuestions.Add(new PlayerQuestion { Player = player, Question = question });
            }

            this.ctx.Update(player);

            await base.ctx.SaveChangesAsync();

            return player.PlayerQuestions;
        }

        public async Task<IQueryable<QuestionResponse>> Handle(QueryQuestionRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.mapper.ProjectTo<QuestionResponse>(ctx.Questions));
        }

        public async Task<IQueryable<QuestionResponse>> Handle(QuerySingleQuestionRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.mapper.ProjectTo<QuestionResponse>(ctx.Questions.Include(q => q.Children).ThenInclude(h => h.Children).Where(v => v.Id == request.key)));
        }
    }
}
