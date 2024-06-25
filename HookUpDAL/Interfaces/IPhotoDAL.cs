using CloudinaryDotNet.Actions;
using HookUpDAL.Entities;
using Microsoft.AspNetCore.Http;

namespace HookUpDAL.Interfaces
{
    public interface IPhotoDAL
    {
        public Task<ImageUploadResult> AddPhotoAsync(string username, IFormFile file);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
        public Task<IEnumerable<Photo>> GetUnapprovedPhotos();
        public Task<Photo> GetPhotoById(int id);
        public void RemovePhoto(Photo photo);
        Task<bool> SaveAllAsync();
    }
}
