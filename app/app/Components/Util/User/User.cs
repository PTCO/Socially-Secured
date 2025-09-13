public class User
{
    public int id { get; set; } = 1; // Default user ID
    public string Username { get; set; } = string.Empty; // Default username
    public string Password { get; set; } = "Brannd101@";
    public string Email { get; set; } = string.Empty; // Default email
    public bool IsMuted { get; set; } = false;
    public List<string> Socials { get; set; } = new List<string>()
    {
        " ",
        " ",
        " "
    };
    public string ProfilePicture { get; set; } = "https://www.bing.com/th/id/OIP.eU8MYLNMRBadK-YgTT6FJQHaHw?w=187&h=211&c=8&rs=1&qlt=90&r=0&o=6&pid=3.1&rm=2"; // Default profile picture URL

    public List<int> BlockedUsers { get; set; } = new List<int>();

    public List<Chat.Chat.ChatUser> Friends { get; set; } = new List<Chat.Chat.ChatUser>();
    public List<Chat.Chat.ChatUser> Strangers { get; set; } = new List<Chat.Chat.ChatUser>();
    public List<Chat.Chat.ChatMessage> Messages { get; set; } = new List<Chat.Chat.ChatMessage>();

    public List<Chat.Chat.ChatRequest> Requests { get; set; } = new List<Chat.Chat.ChatRequest>();
    public List<NotificationMessage> NotificationMessages { get; set; } = new List<NotificationMessage>();
}