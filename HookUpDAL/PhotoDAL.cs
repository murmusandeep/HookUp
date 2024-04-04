﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HookUpDAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HookUpDAL
{
    public class PhotoDAL : IPhotoDAL
    {
        private readonly Cloudinary _cloudinary;

        public PhotoDAL(Cloudinary cloudinary)
        {
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
    }
}
