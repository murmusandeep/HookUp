using Entities.Dto;
using Microsoft.AspNetCore.Http;

namespace HookUpBLL.Interfaces
{
    public interface IPhotoBLL
    {
        public Task<PhotoDto> AddPhotoAsync(string username, IFormFile file);
        public Task<bool> DeletePhotoAsync(string username, int photoId);
        public Task<bool> SetMainPhoto(string username, int photoId);
        public Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
        public Task<bool> RemovePhoto(int photoId);
        public Task<bool> ApprovePhoto(int photoId);
    }
}
