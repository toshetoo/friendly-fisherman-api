using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using Microsoft.Extensions.Options;
using Moq;

namespace Users.Tests.Fixtures
{
    public class AppSettingsFixture
    {
        public IOptions<AppSettings> CreateMockSettings()
        {
            var settings = new Mock<IOptions<AppSettings>>();

            settings.SetupGet(s => s.Value).Returns(new AppSettings() {Secret = "72D8EEB354027D66A16BCEF741D92F53369220C042F43871987E2AEF557AC14" });

            return settings.Object;
        }
    }
}
