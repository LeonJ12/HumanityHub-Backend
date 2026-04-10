namespace HumanityHub.AppExceptions
{
    public class ConflictException : HumanityHubException
    {
        public ConflictException(string message) : base(message, 409)
        {
        }
    }
}
