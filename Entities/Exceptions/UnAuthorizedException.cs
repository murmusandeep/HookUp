namespace Entities.Exceptions
{
    public sealed class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base("Unauthorized") { }
    }
}
