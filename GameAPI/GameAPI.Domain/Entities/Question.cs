using System.Collections.Generic;

namespace GameAPI.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool? YesNo { get; set; }
        public virtual HashSet<PlayerQuestion> PlayerQuestions { get; set; }
        public virtual Question Parent { get; set; }
        public virtual List<Question> Children { get; set; }
    }
}
