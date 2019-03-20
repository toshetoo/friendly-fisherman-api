using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Settings;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FriendlyFisherman.SharedKernel
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public EmailSettings EmailSettings { get; set; }
    }
}
