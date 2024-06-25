using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL
{
    public class PhotoDAL : IPhotoDAL
    {
        private readonly DataContext _dataContext;
        private readonly Cloudinary _cloudinary;

        public PhotoDAL(DataContext dataContext, Cloudinary cloudinary)
        {
            _dataContext = dataContext;
            _cloudinary = cloudinary;
        }
        public async Task<ImageUploadResult> AddPhotoAsync(string username, IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = $"client/{username}"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            return await _dataContext.Photos.IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Photo>> GetUnapprovedPhotos()
        {
            var result = await _dataContext.Photos
                .IgnoreQueryFilters()
                .Where(p => p.IsApproved == false)
                .Include(u => u.AppUser)
                .ToListAsync();
            return result;
        }

        public void RemovePhoto(Photo photo)
        {
            _dataContext.Photos.Remove(photo);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
