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
    public class PriceHistoryServiceTest
    {
        public readonly PriceHistoryService service;
        private readonly Mock<IPriceHistoryRepository> repMoq;

        public PriceHistoryServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IPriceHistoryRepository>();

            repositoryWrapperMoq.Setup(x => x.PriceHistory)
                .Returns(repMoq.Object);

            service = new PriceHistoryService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectPriceHistory()
        {
            return new List<object[]>
            {
                new object[] {new PriceHistory { ProductId = 1,  Price = 0, CreatedBy = 1} }
            };
        }

        [Fact]
        public async void CreateAsync_NullPriceHistory_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<PriceHistory>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewPriceHistory_ShouldCreateNewPriceHistory()
        {
            var example = new PriceHistory()
            {
                ProductId = 1,
                Price = 1,
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<PriceHistory>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectPriceHistory))]
        public async Task CreateAsync_NewPriceHistory_ShouldNotCreateNewPriceHistory(PriceHistory model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<PriceHistory>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectPriceHistoryUpdate()
        {
            return new List<object[]>
            {
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 0, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ChangeDate = DateTime.Now } },
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 2, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.MaxValue, ChangeDate = DateTime.Now } },
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 2, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ChangeDate = DateTime.MaxValue } },
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 2, CreatedBy = 1, IsDeleted = true, CreatedDate = DateTime.Now, ChangeDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 2, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ChangeDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 2, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ChangeDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new PriceHistory { PriceHistoryId = 1, ProductId = 1, Price = 2, CreatedBy = 1, IsDeleted = false, CreatedDate = DateTime.Now, ChangeDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullPriceHistory_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<PriceHistory>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewPriceHistory_ShouldUpdateNewPriceHistory()
        {
            var example = new PriceHistory()
            {
                PriceHistoryId = 1,
                ProductId = 1,
                Price = 2,
                CreatedBy = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                ChangeDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<PriceHistory>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectPriceHistoryUpdate))]
        public async Task UpdateAsync_NewPriceHistory_ShouldNotUpdateNewPriceHistory(PriceHistory model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<PriceHistory>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullPriceHistory_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<PriceHistory, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullPriceHistory_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<PriceHistory>()), Times.Never);
        }
    }
}