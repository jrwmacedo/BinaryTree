using System;
using System.Collections.Generic;

namespace GameAPI.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public virtual List<PlayerQuestion> PlayerQuestions { get; set; }
    }
}
