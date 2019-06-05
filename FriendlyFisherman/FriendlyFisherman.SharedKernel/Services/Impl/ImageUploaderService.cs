using System;
using System.IO;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Requests.Images;
using FriendlyFisherman.SharedKernel.Responses.Images;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.ViewModels;
using Microsoft.Extensions.Options;

namespace FriendlyFisherman.SharedKernel.Services.Impl
{
    public class ImageUploaderService: IImageUploaderService
    {
        private readonly AppSettings _appSettings;

        public ImageUploaderService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request)
        {
            return await Task.Run(() => UploadImage(request));
        }

        private UploadImageResponse UploadImage(UploadImageRequest request)
        {
            var response = new UploadImageResponse();

            try
            {
                var imagePath = $"{request.Id}_{Guid.NewGuid()}";

                string filePath = FileHelper.BuildFilePath(_appSettings.FileUploadSettings.FilesUploadFolder, imagePath);

                if (!File.Exists(filePath))
                {
                    FileHelper.CreateFile(request.ImageSource, filePath);

                    response.Item = new ImageUploadViewModel()
                    {
                        ImageSource = filePath
                    };
                }

            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;

        }
    }
}
