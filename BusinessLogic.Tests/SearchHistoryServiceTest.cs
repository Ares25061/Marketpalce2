using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class SearchHistoryServiceTest
    {
        public readonly SearchHistoryService service;
        private readonly Mock<ISearchHistoryRepository> repMoq;

        public SearchHistoryServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<ISearchHistoryRepository>();

            repositoryWrapperMoq.Setup(x => x.SearchHistory)
                .Returns(repMoq.Object);

            service = new SearchHistoryService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectSearchHistory()
        {
            return new List<object[]>
            {
                new object[] {new SearchHistory {  UserId = 1,  SearchTerm = "", CreatedBy = 1 } },
            };
        }

        [Fact]
        public async void CreateAsync_NullSearchHistory_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<SearchHistory>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewSearchHistory_ShouldCreateNewSearchHistory()
        {
            var example = new SearchHistory()
            {
                UserId = 1,
                SearchTerm = "searchterm",
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<SearchHistory>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectSearchHistory))]
        public async Task CreateAsync_NewSearchHistory_ShouldNotCreateNewSearchHistory(SearchHistory model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<SearchHistory>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectSearchHistoryUpdate()
        {
            return new List<object[]>
            {
                new object[] {new SearchHistory { SearchHistoryId = 1, UserId = 1, SearchTerm = "", CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new SearchHistory { SearchHistoryId = 1, UserId = 1, SearchTerm = "searchterm", CreatedBy = 1, IsDeleted = true, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new SearchHistory { SearchHistoryId = 1, UserId = 1, SearchTerm = "searchterm", CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new SearchHistory { SearchHistoryId = 1, UserId = 1, SearchTerm = "searchterm", CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new SearchHistory { SearchHistoryId = 1, UserId = 1, SearchTerm = "searchterm", CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullSearchHistory_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<SearchHistory>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewSearchHistory_ShouldUpdateNewSearchHistory()
        {
            var example = new SearchHistory()
            {
                SearchHistoryId = 1,
                UserId = 1,
                SearchTerm = "searchterm",
                CreatedBy = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<SearchHistory>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectSearchHistoryUpdate))]
        public async Task UpdateAsync_NewSearchHistory_ShouldNotUpdateNewSearchHistory(SearchHistory model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<SearchHistory>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullSearchHistory_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<SearchHistory, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullSearchHistory_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<SearchHistory>()), Times.Never);
        }
    }
}