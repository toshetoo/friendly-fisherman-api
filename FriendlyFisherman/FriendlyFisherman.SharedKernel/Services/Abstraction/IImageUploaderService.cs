using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Requests.Images;
using FriendlyFisherman.SharedKernel.Responses.Images;

namespace FriendlyFisherman.SharedKernel.Services.Abstraction
{
    public interface IImageUploaderService
    {
        Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request);
    }
}
