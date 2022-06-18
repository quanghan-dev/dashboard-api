namespace Application.Services
{
    public interface IUtilService
    {
        string RandomString();

        DateTime UnixTimeStampToDateTime(long unixTimeStamp);
    }
}