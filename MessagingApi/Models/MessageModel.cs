namespace MessagingApi.Models
{
    public enum Type
    {
        General,        
        Group,
        OneOnOne
    }

    public class MessageModel
    {
        public string Message { get; set; }
        public int SenderId { get; set; }
        public Type Type { get; set; } = Type.General;
        public int TypeId { get; set; }
    }
}
