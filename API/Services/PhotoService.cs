using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data.Helpers;
using DatingApp.API.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            Account acc = new()
            {
                ApiKey = config.Value.APIKey,
                Cloud = config.Value.CloudName,
                ApiSecret = config.Value.APISecret,
            };
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            ImageUploadResult result = new();
            if (file is not null && file.Length > 0)
            {
                using Stream stream = file.OpenReadStream();
                ImageUploadParams @params = new()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                result = await _cloudinary.UploadAsync(@params);
            }

            return result;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            DeletionParams @params = new(publicId);
            DeletionResult result = await _cloudinary.DestroyAsync(@params);
            return result;
        }
    }
}