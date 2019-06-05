using Administration.Domain.Entities.Events;
using FriendlyFisherman.SharedKernel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration.Services.Response
{
    public class SaveImageCoverResponse : ServiceResponseBase<Event>
    {
        public string ImageCoverFilePath { get; set; }
    }
}
