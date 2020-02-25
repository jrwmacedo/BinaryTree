namespace GameAPI.Domain.Entities
{
    public class PlayerQuestion
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
