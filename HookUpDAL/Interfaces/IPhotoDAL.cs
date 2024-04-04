using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace HookUpDAL.Interfaces
{
    public interface IPhotoDAL
    {
        public Task<ImageUploadResult> AddPhotoAsync(string username, IFormFile file);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
