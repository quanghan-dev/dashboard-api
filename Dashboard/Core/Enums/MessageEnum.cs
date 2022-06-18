namespace Core.Enums
{
    public enum ValidatorMessage
    {
        Invalid_Email,
        Used_Email,
        Invalid_Username,
        Used_Username,
        Invalid_Password_Length,
        Invalid_Username_Password,
        Unauthorized
    }

    public enum ServiceMessage
    {
        Succeeded_Register,
        Invalid_Activate_Code,
        Activated_Account,
        Activate_Message,
    }

    public enum ErrorMessage
    {
        Invalid_Token
    }
}