using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.EntityViewModels;

namespace Users.Services.Request
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
