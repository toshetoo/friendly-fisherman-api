using System;
using System.Collections.Generic;
using System.Text;
using Users.DataAccess.Repositories;
using Users.Domain.Entities;
using Users.Tests.Fixtures;
using Users.Tests.TestData;
using Xunit;

namespace Users.Tests.RepositoryTests
{
    public class PersonalMessagesRepositoryTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public PersonalMessagesRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_BuildsCorrectly()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            Assert.NotNull(new PersonalMessagesRepository(null));
            Assert.NotNull(GetRepo(data));
        }

        private PersonalMessagesRepository GetRepo(List<PersonalMessage> data)
        {
            var mockSet = _dbSetFixture.CreateMockSet<PersonalMessage>(data);
            var mockContext = _contextFixture.CreateMockContext<PersonalMessage>(mockSet).Object;

            return new PersonalMessagesRepository(mockContext);
        }
    }
}
