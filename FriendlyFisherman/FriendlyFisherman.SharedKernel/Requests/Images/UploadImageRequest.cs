using FriendlyFisherman.SharedKernel.ViewModels;

namespace FriendlyFisherman.SharedKernel.Requests.Images
{
    public class UploadImageRequest
    {
        public string Id { get; set; }
        public string ImageSource { get; set; }

        public UploadImageRequest(ImageUploadViewModel model)
        {
            Id = model.Id;
            ImageSource = model.ImageSource;
        }
    }
}
