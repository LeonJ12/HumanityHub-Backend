namespace HumanityHub.AppExceptions
{
    public class BadRequestException : HumanityHubException
    {
        public BadRequestException(string message) : base(message, 400)
        {
        }
    }
}
