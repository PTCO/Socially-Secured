using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
public class Model
{
    // Dynamically adds a property to the Model class using a dictionary
    private Dictionary<string, object> _dynamicProperties = new Dictionary<string, object>();
    public List<object> properties = new List<object>(); // List to store all properties of the Model class
    public class UI // Class to manage the UI state - will change the visibility of UI elements based on the current state
    {
        public string UI_UsernameCheck { get; set; } = "flex"; 
        public string UI_PasswordCheck { get; set; } = "hidden";
        public string UI_EmailCheck { get; set; } = "hidden";
        public string UI_Forgot { get; set; } = "hidden";
        public string UI_EmailVerification { get; set; } = "hidden";
        public string UI_ChangeUsername { get; set; } = "hidden";
        public string UI_ChangePassword { get; set; } = "hidden";
        public string UI_Errors { get; set; } = "hidden";
        public string UI_Select_Forgot_Option { get; set; } = "hidden";
        public string UI_Recover { get; set; } = "hidden";
        public string UI_None { get; set; } = "hidden";
        public string UI_Welcome { get; set; } = "hidden";
    }
    public static string status { get; set; } = "Invalid"; // Placeholder for username check status
    public static int pwdStrength { get; set; } = 0; // Placeholder for username check status
    public static List<string> errorMessages = new List<string>();
    public static string initialUIView = ""; // Initial UI view to be displayed
    public static UI uI = new UI();


    // Functions to manage the Model class properties and UI state
    public void AddProperty(string key, object value) // Add a new property to the Model class
    {
        if (!_dynamicProperties.ContainsKey(key))
        {
            _dynamicProperties.Add(key, value);
        }
        else
        {
            _dynamicProperties[key] = value;
        }
    }
    public string GetProperty(string key) // Get the value of a property
    {
        if (_dynamicProperties.ContainsKey(key))
        {
            return _dynamicProperties[key]?.ToString();
        }
        return null;
    }
    public void UpdateProperty(string key, string value) // Update the value of a property
    {
        if (_dynamicProperties.ContainsKey(key))
        {
            _dynamicProperties[key] = value;
        }
        else
        {
            _dynamicProperties.Add(key, value);
        }
    }
    public object GetAllProperties() // Get all properties of the Model class
    {
        return _dynamicProperties;
    }
    public static string Validation(string type, string value, string errorMsg, string matchValue) // Validate the input based on the type
    {
        string pattern = string.Empty; // Initialize the pattern to check with regex
        if (errorMessages.Contains(errorMsg))
        {
            errorMessages.Remove(errorMsg); // Remove the error message if it already exists
        }
        switch (type)
        {
            case "empty":
                if (string.IsNullOrWhiteSpace(value)) // Check if the value is empty
                {
                    errorMessages.Add(errorMsg);
                    return errorMsg;
                }
                break;
            case "match":
                Console.WriteLine("Match Value: " + matchValue);
                if (value != matchValue) // Check if the value matches the given value
                {
                    return errorMsg;
                }
                break;
            case "email":
                pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                break;
            case "phone":
                pattern = @"^\+?[1-9]\d{1,14}$";
                break;
            case "url":
                pattern = @"^(http|https)://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/S*)?$";
                break;
            case "date":
                pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";
                break;
            case "password":
                pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&_]+$";
                break;
            case "username":
                pattern = @"^[a-zA-Z0-9_]{3,16}$";
                break;
            default:
                return "Invalid type";
        }

        if (!Regex.IsMatch(value, pattern)) // Check if the value matches the pattern
        {
            return errorMsg;
        }
        return "";
    }
    public static void SwitchUI(string text) // Switch the UI based on the text parameter
    {
        initialUIView = text != "Errors" ? text : initialUIView; // Set the initial UI view if not already set
        foreach (var ui in uI.GetType().GetProperties())
        {
            ui.SetValue(uI, "hidden");
        }
        var property = uI.GetType().GetProperty("UI_" + text);
        if (property != null)
        {
            property.SetValue(uI, "!flex");
        }
    }
    public static void removeError(string error) // Remove an error message from the list
    {
        if (errorMessages.Contains(error))
        {
            errorMessages.Remove(error);
        }
    }
    public static bool CheckPasswordStrength(string password) // Check the password strength and validity
    {
        pwdStrength = 0;
        SwitchUI("PasswordCheck");
        
        status = "Invalid";
        if (string.IsNullOrWhiteSpace(password))
        {
            removeError("Password contains invalid characters.");
            removeError("Password must be at least 8 characters long.");
            errorMessages.Add("Password cannot be empty.");
            return true;
        }
        else
        {
            removeError("Password cannot be empty.");
        }

        if (!Regex.IsMatch(password, @"^[^()*&^#$%~`/\\|]+$"))
        {
            if (!errorMessages.Contains("Password contains invalid characters."))
            {
                errorMessages.Add("Password contains invalid characters.");
            }
            SwitchUI("Errors");
            return true;
        }
        else
        {
            removeError("Password contains invalid characters.");
        }


        if (password.Length < 8) // Check if the password is at least 8 characters long
        {
            if (!errorMessages.Contains("Password must be at least 8 characters long."))
                errorMessages.Add("Password must be at least 8 characters long.");
        }
        else
        {
            removeError("Password must be at least 8 characters long.");
        }

        if (password.Length >= 8)
        {
            pwdStrength += 1;
            status = "Weak";
            if (password.Length >= 12 && password.Length <= 20)
            {
                pwdStrength += 1;
            }
        }

        if (Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&_]+$") && password.Length > 8)
        {
            pwdStrength += 3;
            status = "Strong";
        }
        return false;
    }
    public static bool CheckUsername(string username, List<string> takenUsernames) // Check the username for validity
    {
        SwitchUI("UsernameCheck");
        status = "Invalid";
        if (string.IsNullOrWhiteSpace(username))
        {
            removeError("Username must be between 8 and 12 characters.");
            errorMessages.Add("Username cannot be empty.");
            return true;
        }
        else
        {
            removeError("Username cannot be empty.");
        }

        if (username.Length < 8 || username.Length > 12)
        {
            if (!errorMessages.Contains("Username must be between 8 and 12 characters."))
            {
                errorMessages.Add("Username must be between 8 and 12 characters.");
            }
            SwitchUI("Errors");
            return true;
        }
        else
        {
            removeError("Username must be between 8 and 12 characters.");
        }

        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
        {
            if (!errorMessages.Contains("Username can only contain letters and numbers."))
            {
                errorMessages.Add("Username can only contain letters and numbers.");
            }
            SwitchUI("Errors");
            return true;
        }
        else
        {
            removeError("Username can only contain letters and numbers.");
        }

        if (takenUsernames.Contains(username))
        {
            status = "Taken";
            return true;
        }
        else
        {
            status = "Available";
        }
        return false;
    }
    public static bool CheckEmail(string email, List<string> takenemails)
    {
        SwitchUI("EmailCheck");
        if (string.IsNullOrWhiteSpace(email))
        {
            status = "Invalid";
            removeError("Invalid email format.");
            errorMessages.Add("Email cannot be empty.");
            return true;
        }
        else
        {
            removeError("Email cannot be empty.");
        }

        if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            if (!errorMessages.Contains("Invalid email format."))
            {
                errorMessages.Add("Invalid email format.");
            }
            SwitchUI("Errors");
            return true;
        }
        else
        {
            removeError("Invalid email format.");
        }
        
        if (takenemails.Contains(email))
        {
            status = "Taken";
            return true;
        }
        else
        {
            status = "Available";
        }
        return false;
    }
    public static void CheckFile(Microsoft.AspNetCore.Components.Forms.IBrowserFile file) // Check the file type and size
    {
        var ContentTypes = new List<string> { "image/jpg" }; // List of allowed content types
        if (file.Size > 1000)
        {
            errorMessages.Add("File size is too large");
        }
        else
        {
            removeError("File size is too large");
        }

        foreach (var type in ContentTypes) {
            if (!file.ContentType.Contains(type))
            {
                errorMessages.Add("Invalid file type");
                return;
            }
            else
            {
                removeError("Invalid file type");
                return;
            }
        }
    }
};


