using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Repositories.Events;
using Administration.Tests.Fixtures;
using FriendlyFisherman.SharedKernel;
using Users.Domain.Repositories;

namespace Administration.Tests.ServiceTests
{
    public class EventsServiceTests
    {
        private readonly IEventParticipantsRepository _participantsRepository;
        private readonly IEventCommentsRepository _eventCommentsRepository;
        private readonly IEventsRepository _eventsRepository;
        private readonly IUserRepository _usersUserRepository;
        private readonly AppSettings _appSettings;
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;
        private readonly AppSettingsFixture _settingsFixture;

        public EventsServiceTests(ContextFixture contextFixture, DbSetFixture dbSetFixture, RepositoryFixture repositoryFixture, AppSettingsFixture settingsFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
            _settingsFixture = settingsFixture;

            
        }
    }
}
