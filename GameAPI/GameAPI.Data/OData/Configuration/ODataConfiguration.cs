using GameAPI.Data.Response;
using GameAPI.Domain.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameAPI.Data.OData.Configuration
{
    public static class ODataConfiguration
    {
        public static IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Player>("Players");
            odataBuilder.EntitySet<Question>("Questions");

            return odataBuilder.GetEdmModel();
        }
    }
}
