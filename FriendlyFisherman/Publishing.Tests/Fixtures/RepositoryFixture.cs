using System;
using System.Linq;
using System.Linq.Expressions;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Publishing.Tests.Fixtures
{
    public class RepositoryFixture
    {
        public TR CreateRepository<TR, T>(DbContext context) where  TR: BaseRepository<T> where T : BaseEntity
        {
            var dbSet = context.Set<T>();
            var repositoryMock = new Mock<TR>();

            repositoryMock.Setup(repo => repo.Create(It.IsAny<T>())).Callback((T item) => { dbSet.Add(item); });
            repositoryMock.Setup(repo => repo.Delete(It.IsAny<T>())).Callback((T item) => { dbSet.Remove(item); });
            repositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<T, object>>[]>()))
                .Returns((Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] collection) => dbSet.FirstOrDefault(filter));
            repositoryMock.Setup(repo => repo.GetAll(It.IsAny<bool>(), It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<T, object>>[]>()))
                .Returns((Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] collection) => dbSet.Where(filter));
            repositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<T, object>>[]>()))
                .Returns((Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] collection) => dbSet.Where(filter));

            return repositoryMock.Object;
        }
    }
}
