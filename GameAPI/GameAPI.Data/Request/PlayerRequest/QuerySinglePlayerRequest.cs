using GameAPI.Data.Response.PlayerResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameAPI.Data.Request.PlayerRequest
{
    public class QuerySinglePlayerRequest : IRequest<IQueryable<PlayerResponse>>
    {
        public int key { get; set; }
    }
}
