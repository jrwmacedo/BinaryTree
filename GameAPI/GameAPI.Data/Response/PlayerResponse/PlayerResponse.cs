using System;
using System.Collections.Generic;
using System.Text;

namespace GameAPI.Data.Response.PlayerResponse
{
    public class PlayerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public List<AnswerResponse> PlayerQuestions { get; set; }
    }
}
