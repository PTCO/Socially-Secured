public class UserService
{
    public User? CurrentUser { get; private set; }

    public void SetUser(string userData)
    {
        var user = System.Text.Json.JsonSerializer.Deserialize<User>(userData);
        CurrentUser = user;
    }

    public void ClearUser()
    {
        CurrentUser = null;
    }

    public bool IsAuthenticated => CurrentUser != null;
}