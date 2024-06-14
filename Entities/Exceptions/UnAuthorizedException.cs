namespace Entities.Exceptions
{
    public sealed class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message) : base(message) { }
    }
}
