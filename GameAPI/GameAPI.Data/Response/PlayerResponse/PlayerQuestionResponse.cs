using System;
using System.Collections.Generic;
using System.Text;

namespace GameAPI.Data.Response.PlayerResponse
{
    public class PlayerQuestionResponse
    {
        public int Id { get; set; }
        public int[] Answers { get; set; }
    }
}
