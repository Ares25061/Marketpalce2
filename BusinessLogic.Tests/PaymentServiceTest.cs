using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Linq.Expressions;

namespace BuisnessLogic.Tests
{
    public class PaymentServiceTest
    {

        public readonly PaymentService service;
        private readonly Mock<IPaymentRepository> repMoq;

        public PaymentServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IPaymentRepository>();

            repositoryWrapperMoq.Setup(x => x.Payment)
                .Returns(repMoq.Object);

            service = new PaymentService(repositoryWrapperMoq.Object);
        }


        public static IEnumerable<object[]> GetIncorrectPayment()
        {
            return new List<object[]>
            {
                new object[] {new Payment { CardNumber = "-123456789012345", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "333"  } },
                new object[] {new Payment { CardNumber = "", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "333"  } },
                new object[] {new Payment { CardNumber = "adssdadsa", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "333"  } },
                new object[] {new Payment { CardNumber = "1234567890123456", ExpressionDate = DateTime.MinValue, Cvv = "333"  } },
                new object[] {new Payment { CardNumber = "1234567890123456", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "-23"  } },
                new object[] {new Payment { CardNumber = "1234567890123456", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "ads"  } },
                new object[] {new Payment { CardNumber = "1234567890123456", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = ""  } },

            };
        }


        [Fact]
        public async void CreateAsync_NullPayment_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Payment>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewPayment_ShouldCreateNewPayment()
        {
            var example = new Payment()
            {
                CardNumber = "1234567890123456",
                ExpressionDate = DateTime.MaxValue,
                Cvv = "333"
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Payment>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectPayment))]
        public async Task CreateAsync_NewPayment_ShouldNotCreateNewPayment(Payment model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Payment>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }


        public static IEnumerable<object[]> GetIncorrectPaymentUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Payment { PaymentId = 1, CardNumber = "-123456789012345", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "333"  } },
                new object[] {new Payment { PaymentId = 1, CardNumber = "", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "333"  } },
                new object[] {new Payment { PaymentId = 1, CardNumber = "adssdadsa", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "333"  } },
                new object[] {new Payment { PaymentId = 1, CardNumber = "1234567890123456", ExpressionDate = DateTime.MinValue, Cvv = "333"  } },
                new object[] {new Payment { PaymentId = 1, CardNumber = "1234567890123456", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "-23"  } },
                new object[] {new Payment { PaymentId = 1, CardNumber = "1234567890123456", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = "ads"  } },
                new object[] {new Payment { PaymentId = 1, CardNumber = "1234567890123456", ExpressionDate = DateTime.ParseExact("22-10-2027", "dd-MM-yyyy", null), Cvv = ""  } },

            };
        }


        [Fact]
        public async void UpdateAsync_NullPayment_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Payment>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewPayment_ShouldUpdateNewPayment()
        {
            var example = new Payment()
            {
                PaymentId = 1,
                CardNumber = "1234567890123456",
                ExpressionDate = DateTime.MaxValue,
                Cvv = "333"
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Payment>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectPayment))]
        public async Task UpdateAsync_NewPayment_ShouldNotUpdateNewPayment(Payment model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Payment>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullPayment_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Payment, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullPayment_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Payment>()), Times.Never);
        }
    }
}