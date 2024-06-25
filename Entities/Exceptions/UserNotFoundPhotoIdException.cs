namespace Entities.Exceptions
{
    public class UserNotFoundPhotoIdException : NotFoundException
    {
        public UserNotFoundPhotoIdException(int photoId) : base($"The User with Photo Id: {photoId} doesn't exist in the database.") { }
    }
}
