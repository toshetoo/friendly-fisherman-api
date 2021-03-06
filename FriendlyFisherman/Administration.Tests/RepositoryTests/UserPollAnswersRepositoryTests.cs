﻿using System;
using System.Collections.Generic;
using System.Text;
using Administration.DataAccess.Repositories.Polls;
using Administration.Domain.Entities.Polls;
using Administration.Tests.Fixtures;
using Xunit;

namespace Administration.Tests.RepositoryTests
{
    public class UserPollAnswersRepositoryTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public UserPollAnswersRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithContext_BuildsCorrectly()
        {
            var repo = new UserPollAnswersRepository(_contextFixture.CreateMockContext(_dbSetFixture.CreateMockSet(new List<UserPollAnswer>())).Object);
            Assert.NotNull(repo);
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new UserPollAnswersRepository(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }
    }
}
