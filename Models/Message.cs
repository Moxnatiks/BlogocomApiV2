namespace BlogocomApiV2.Models
{
    public class Message : BaseModel
    {
        public long ChatId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }

        public Chat Chat { get; set; }
        public User User { get; set; }

    }
}
