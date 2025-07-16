namespace Chat
{
    public class Chat
    {
        public int ChatID { get; set; }
        public void SetCurrentChat(int chatID)
        {
            ChatID = chatID;
        }

        public class ChatUser
        {
            public int id { get; set; }
            public string username { get; set; }
            public string ProfilePicture { get; set; }
            public string NamePlateClass { get; set; } = ""; // Default class

            public bool IsSelected { get; set; } = false;
        }

        public class ChatMessage
        {
            public int id { get; set; }
            public int SenderID { get; set; }
            public string body { get; set; }
            public DateTime Timestamp { get; set; }
            public bool IsRead { get; set; }
        }

        public class ChatRequest
        {
            public int id {get; set;}
            public string Username { get; set; }
            public string ProfilePicture { get; set; }
        }

        public static List<ChatUser> Users { get; set; } = new List<ChatUser>();
        public static List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public static List<ChatRequest> Requests { get; set; } = new List<ChatRequest>()
        {
            new ChatRequest {id = 1, Username = "Userdaaaaaaa", ProfilePicture = "images/User1.png" },
            new ChatRequest {id = 2, Username = "User2", ProfilePicture = "images/User2.png" },
            new ChatRequest {id = 3, Username = "User3", ProfilePicture = "images/User3.png" },
            new ChatRequest {id = 4, Username = "User3", ProfilePicture = "images/User3.png" },
            new ChatRequest {id = 5, Username = "User3", ProfilePicture = "images/User3.png" },
            new ChatRequest {id = 6, Username = "User3", ProfilePicture = "images/User3.png" },
            new ChatRequest {id = 7, Username = "User3", ProfilePicture = "images/User3.png" }
        };
    }
}