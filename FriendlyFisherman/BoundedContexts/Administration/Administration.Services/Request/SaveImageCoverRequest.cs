using Administration.Domain.Entities.Events;
using FriendlyFisherman.SharedKernel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration.Services.Request
{
    public class SaveImageCoverRequest : ServiceRequestBase<Event>
    {
        public SaveImageCoverRequest(string imageName, string imageData)
        {
            ImageName = imageName;
            ImageData = imageData;
        }
        public string ImageName { get; set; }
        public string ImageData { get; set; }
    }
}
