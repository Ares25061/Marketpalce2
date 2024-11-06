using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Linq.Expressions;

namespace BuisnessLogic.Tests
{
    public class PaymentUserServiceTest
    {

        public readonly PaymentUserService service;
        private readonly Mock<IPaymentUserRepository> repMoq;

        public PaymentUserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IPaymentUserRepository>();

            repositoryWrapperMoq.Setup(x => x.PaymentUser)
                .Returns(repMoq.Object);

            service = new PaymentUserService(repositoryWrapperMoq.Object);
        }



        [Fact]
        public async void CreateAsync_NullPaymentUser_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<PaymentUser>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewPaymentUser_ShouldCreateNewPaymentUser()
        {
            var example = new PaymentUser()
            {
                PaymentId = 1,
                UserId = 1,
                IsActive = true
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<PaymentUser>()), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_NullPaymentUser_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<PaymentUser>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewPaymentUser_ShouldUpdateNewPaymentUser()
        {
            var example = new PaymentUser()
            {
                PaymentId = 1,
                UserId = 1,
                IsActive = true
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<PaymentUser>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsync_NullPaymentUser_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1, -1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<PaymentUser, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullPaymentUser_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1, -1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<PaymentUser>()), Times.Never);
        }
    }
}