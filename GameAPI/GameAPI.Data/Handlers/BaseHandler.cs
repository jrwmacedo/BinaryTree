using AutoMapper;
using GameAPI.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameAPI.Data.Handlers
{
    public class BaseHandler
    {
        protected readonly IMapper mapper;
        protected readonly DataContext ctx;
        protected BaseHandler(DataContext ctx, IMapper mapper)
        {
            this.mapper = mapper;
            this.ctx = ctx;
        }
    }
}
