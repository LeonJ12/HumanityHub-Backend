namespace HumanityHub.AppExceptions
{
    public class NotFoundException : HumanityHubException
    {
        public NotFoundException(string message) : base(message, 404)
        {
        }
    }
}
