namespace Application.Services.Impl
{
    public class UtilService : IUtilService
    {
        public UtilService()
        {
        }

        public string RandomString() => Guid.NewGuid().ToString();

    }
}