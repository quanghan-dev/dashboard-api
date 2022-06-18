namespace Core.Enums
{
    public class Message
    {
        public static string GetMessage(ServiceMessage message) => message switch
        {
            ServiceMessage.Succeeded_Register => "Your account is activated",
            ServiceMessage.Invalid_Activate_Code => "Invalid activate code",
            ServiceMessage.Activated_Account => "Your account is activated",
            ServiceMessage.Activate_Message => "Please activate your account via the link sent to your email.",
            _ => throw new ArgumentException(message: "Invalid message")
        };

        public static string GetMessage(ValidatorMessage message) => message switch
        {
            ValidatorMessage.Invalid_Email => "Email address is not valid",
            ValidatorMessage.Invalid_Username => "Username is not valid",
            ValidatorMessage.Invalid_Password_Length => "Password should have minimum 8 characters",
            ValidatorMessage.Used_Email => "Email address is already in use",
            ValidatorMessage.Used_Username => "Username is already in use",
            ValidatorMessage.Invalid_Username_Password => "Invalid username or password",
            ValidatorMessage.Unauthorized => "Unauthorized User",
            _ => throw new ArgumentException(message: "Invalid message")
        };

        public static string GetMessage(ErrorMessage message) => message switch
        {
            ErrorMessage.Invalid_Token => "Invalid Token",

            _ => throw new ArgumentException(message: "Invalid message")
        };
    }
}