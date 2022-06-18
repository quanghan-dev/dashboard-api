namespace Application.Services.Impl
{
    public class UtilService : IUtilService
    {
        public UtilService()
        {
        }

        public string RandomString() => Guid.NewGuid().ToString();

        public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }
    }
}