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

namespace BuisnessLogic.Tests
{
    public class UserDiscountServiceTest
    {

        public readonly UserDiscountService service;
        private readonly Mock<IUserDiscountRepository> repMoq;

        public UserDiscountServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IUserDiscountRepository>();

            repositoryWrapperMoq.Setup(x => x.UserDiscount)
                .Returns(repMoq.Object);

            service = new UserDiscountService(repositoryWrapperMoq.Object);
        }



        [Fact]
        public async void CreateAsync_NullUserDiscount_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<UserDiscount>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewUserDiscount_ShouldCreateNewUserDiscount()
        {
            var example = new UserDiscount()
            {
                UserId = 1,
                DiscountId = 1
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<UserDiscount>()), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_NullUserDiscount_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<UserDiscount>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewUserDiscount_ShouldUpdateNewUserDiscount()
        {
            var example = new UserDiscount()
            {
                UserId = 1,
                DiscountId = 1
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<UserDiscount>()), Times.Once);
        }
        [Fact]
        public async void GetByIdAsync_NullUserDiscount_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserDiscount, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullUserDiscount_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<UserDiscount>()), Times.Never);
        }

    }
}