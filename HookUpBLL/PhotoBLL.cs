using AutoMapper;
using Entities.Dto;
using Entities.Exceptions;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HookUpBLL
{
    public class PhotoBLL : IPhotoBLL
    {
        private readonly IPhotoDAL _photoDAL;
        private readonly IUsersDAL _usersDAL;
        private readonly IMapper _mapper;
        public PhotoBLL(IPhotoDAL photoDAL, IUsersDAL usersDAL, IMapper mapper)
        {
            _photoDAL = photoDAL;
            _usersDAL = usersDAL;
            _mapper = mapper;
        }

        public async Task<PhotoDto> AddPhotoAsync(string username, IFormFile file)
        {
            var user = await CheckIfUserExists(username);

            var result = await _photoDAL.AddPhotoAsync(username, file);

            if (result.Error != null)
                throw new BadRequestException(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if (user.Photos.Count == 0) photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _usersDAL.SaveAllAsync())
            {
                return _mapper.Map<PhotoDto>(photo);
            }

            throw new BadRequestException("Problem adding photo");
        }

        public async Task<bool> DeletePhotoAsync(string username, int photoId)
        {
            var user = await CheckIfUserExists(username);

            var photo = await CheckIfPhotoExists(user, photoId);

            if (photo.IsMain) throw new BadRequestException("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoDAL.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) throw new BadRequestException(result.Error.Message);
            }

            user.Photos.Remove(photo);

            return await _usersDAL.SaveAllAsync();
        }

        public async Task<bool> SetMainPhoto(string username, int photoId)
        {
            var user = await CheckIfUserExists(username);

            var photo = await CheckIfPhotoExists(user, photoId);

            if (photo.IsMain) throw new BadRequestException("Photo is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;

            return await _usersDAL.SaveAllAsync();
        }

        public async Task<AppUser> CheckIfUserExists(string username)
        {
            var user = await _usersDAL.GetUserByUsername(username);

            if (user is null)
                throw new UserNotFoundException(username);

            return user;
        }

        public async Task<Photo> CheckIfPhotoExists(AppUser user, int photoId)
        {
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo is null)
                throw new PhotoNotFoundException(photoId);

            return photo;
        }
    }
}
