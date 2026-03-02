namespace HumanityHub.AppExceptions
{
    public abstract class HumanityHubException : Exception
    {
        public int StatusCode { get; }
        public HumanityHubException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
