using GameAPI.Data.Response.QuestionResponse;
using MediatR;
using System.Linq;

namespace GameAPI.Data.Request.QuestionRequest
{
    public class QueryQuestionRequest : IRequest<IQueryable<QuestionResponse>>
    {
    }
}
