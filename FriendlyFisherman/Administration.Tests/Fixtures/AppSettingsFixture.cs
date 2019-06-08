using AutoFixture;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Settings;
using Microsoft.Extensions.Options;
using Moq;

namespace Administration.Tests.Fixtures
{
    public class AppSettingsFixture
    {
        public IOptions<AppSettings> CreateMockSettings()
        {
            var settings = new Mock<IOptions<AppSettings>>();
            var mockMailSettings = new Fixture().Build<EmailSettings>().Create();
            var mockFileSettings = new Fixture().Build<FileUploadSettings>().Create();

            settings.SetupGet(s => s.Value).Returns(new AppSettings()
            {
                Secret = "72D8EEB354027D66A16BCEF741D92F53369220C042F43871987E2AEF557AC14",
                FileUploadSettings = mockFileSettings,
                EmailSettings = mockMailSettings
            });

            return settings.Object;
        }
    }
}
