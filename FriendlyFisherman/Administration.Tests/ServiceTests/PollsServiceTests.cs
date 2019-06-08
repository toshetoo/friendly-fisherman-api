using System;
using System.Collections.Generic;
using System.Text;
using Administration.DataAccess.Repositories.Polls;
using Administration.Domain.Entities.Polls;
using Administration.Domain.Repositories.Polls;
using Administration.Services.Abstraction.Polls;
using Administration.Tests.DummyData;
using Administration.Tests.Fixtures;
using Users.Domain.Entities;
using Xunit;

namespace Administration.Tests.ServiceTests
{
    public class PollsServiceTests : IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>, IClassFixture<RepositoryFixture>
    {
        private readonly IPollsService _service;
        private readonly IPollsRepository _repositoryMock;
        private readonly IUserPollAnswersRepository _answersRepository;
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public PollsServiceTests(ContextFixture contextFixture, DbSetFixture dbSetFixture, RepositoryFixture repositoryFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;

            var data = new TestData().GetPollsData();
            var mockSet = _dbSetFixture.CreateMockSet<Poll>(data);
            var mockContext = _contextFixture.CreateMockContext<Poll>(mockSet).Object;
            _repositoryMock = repositoryFixture.CreateRepository<PollsRepository, Poll>(mockContext);

            var answersData = new TestData().GetUserPollAnswers();
            var answersSet = _dbSetFixture.CreateMockSet<UserPollAnswer>(answersData);
            var answersContext = _contextFixture.CreateMockContext<UserPollAnswer>(answersSet).Object;
            _answersRepository = repositoryFixture.CreateRepository<UserPollAnswersRepository, UserPollAnswer>(answersContext);
        }


    }
}
