using GameAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameAPI.Data.Response.QuestionResponse
{
    public class QuestionResponse
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public bool? YesNo { get; set; }
        public int? ParentId { get; set; }
        public List<Question> Children { get; set; }

    }
}
