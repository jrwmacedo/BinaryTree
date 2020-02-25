using GameAPI.Data.Response.PlayerResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameAPI.Data.Request.PlayerRequest
{
    public class QueryPlayerRequest : IRequest<IQueryable<PlayerResponse>>
    {
    }
}
