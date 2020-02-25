using AutoMapper;
using GameAPI.Data.Context;
using GameAPI.Domain.Entities;
using Microsoft.AspNet.OData;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using GameAPI.Data.Response.QuestionResponse;
using GameAPI.Data.Request.QuestionRequest;

namespace GameAPI.Controllers.QuestionController
{
    public class QuestionsController : ODataController
    {
        private readonly IMediator mediator;

        public QuestionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [EnableQuery]
        public async Task<IQueryable<QuestionResponse>> GetQuestions(QueryQuestionRequest request)
        {
            return await mediator.Send(request);
        }

        [EnableQuery]
        public async Task<SingleResult<QuestionResponse>> GetQuestion(QuerySingleQuestionRequest request)
        {
            var question = await mediator.Send(request);
            return new SingleResult<QuestionResponse>(question);
        }
    }
}
