using GameAPI.Data.Response.QuestionResponse;
using MediatR;
using System.Linq;

namespace GameAPI.Data.Request.QuestionRequest
{
    public class QuerySingleQuestionRequest : IRequest<IQueryable<QuestionResponse>>
    {
        public int key { get; set; }
    }
}
