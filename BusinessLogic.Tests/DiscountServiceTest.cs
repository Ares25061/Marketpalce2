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
    public class DiscountServiceTest
    {
        public readonly DiscountService service;
        private readonly Mock<IDiscountRepository> repMoq;

        public DiscountServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IDiscountRepository>();

            repositoryWrapperMoq.Setup(x => x.Discount)
                .Returns(repMoq.Object);

            service = new DiscountService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectDiscount()
        {
            return new List<object[]>
            {
                new object[] {new Discount { DiscountCode = "", DiscountPercentage = 0, StartDate = DateTime.Now, CreatedBy = 1} },
                new object[] {new Discount { DiscountCode = "discountcode", DiscountPercentage = 0, StartDate = DateTime.Now, CreatedBy = 1} },
                new object[] {new Discount { DiscountCode = "discountcode", DiscountPercentage = 101, StartDate = DateTime.Now, CreatedBy = 1} },
                new object[] {new Discount { DiscountCode = "discountcode", DiscountPercentage = 30, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now, CreatedBy = 1} }
            };
        }

        [Fact]
        public async void CreateAsync_NullDiscount_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Discount>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewDiscount_ShouldCreateNewDiscount()
        {
            var example = new Discount()
            {
                DiscountCode = "discountcode",
                DiscountPercentage = 15,
                StartDate = DateTime.Now,
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Discount>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectDiscount))]
        public async Task CreateAsync_NewDiscount_ShouldNotCreateNewDiscount(Discount model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Discount>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectDiscountUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Discount { DiscountId = 1, DiscountCode = "", DiscountPercentage = 0, StartDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1 } },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 0, StartDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1 } },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 101, StartDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1 } },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 30, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1 } },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 30, StartDate = DateTime.Now, IsDeleted = true, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = null, CreatedBy = 1 } },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 30, StartDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null, CreatedBy = 1 } },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 30, StartDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now,CreatedBy = 1} },
                new object[] {new Discount { DiscountId = 1, DiscountCode = "discountcode", DiscountPercentage = 30, StartDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.MaxValue,CreatedBy = 1} },
            };
        }


        [Fact]
        public async void UpdateAsync_NullDiscount_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Discount>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewDiscount_ShouldUpdateNewDiscount()
        {
            var example = new Discount()
            {
                DiscountId = 1,
                DiscountCode = "discountcode",
                DiscountPercentage = 30,
                StartDate = DateTime.Now,
                CreatedBy = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                DeletedBy = null,
                DeletedDate = null

            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Discount>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectDiscountUpdate))]
        public async Task UpdateAsync_NewDiscount_ShouldNotUpdateNewDiscount(Discount model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Discount>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullDiscount_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Discount, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullDiscount_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Discount>()), Times.Never);
        }
    }
}