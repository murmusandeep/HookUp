namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string username) : base($"The User with username: {username} doesn't exist in the database.") { }
    }
}
