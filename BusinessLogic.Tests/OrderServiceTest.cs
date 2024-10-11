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
    public class OrderServiceTest
    {
        public readonly OrderService service;
        private readonly Mock<IOrderRepository> repMoq;

        public OrderServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IOrderRepository>();

            repositoryWrapperMoq.Setup(x => x.Order)
                .Returns(repMoq.Object);

            service = new OrderService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectOrder()
        {
            return new List<object[]>
            {
                new object[] {new Order { BuyerId = 1, OrderDate = DateTime.Now, Status = "", CreatedBy = 1} },
                new object[] {new Order { BuyerId = 1, OrderDate = DateTime.MaxValue, Status = "", CreatedBy = 1} },
            };
        }

        [Fact]
        public async void CreateAsync_NullOrder_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Order>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewOrder_ShouldCreateNewOrder()
        {
            var example = new Order()
            {
                BuyerId = 1,
                OrderDate = DateTime.Now,
                Status = "status",
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Order>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectOrder))]
        public async Task CreateAsync_NewOrder_ShouldNotCreateNewOrder(Order model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Order>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectOrderUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.MaxValue,  IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.MaxValue, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = false, CreatedDate = DateTime.MaxValue } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = true, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = null  } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null  } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now  } },
                new object[] {new Order { OrderId = 1, BuyerId = 1, OrderDate = DateTime.Now, Status = "status", CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now,  IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue  } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullOrder_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Order>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewOrder_ShouldUpdateNewOrder()
        {
            var example = new Order()
            {
                OrderId = 1,
                BuyerId = 1,
                OrderDate = DateTime.Now,
                Status = "status",
                CreatedBy = 1,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Order>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectOrderUpdate))]
        public async Task UpdateAsync_NewOrder_ShouldNotUpdateNewOrder(Order model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Order>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullOrder_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullOrder_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Order>()), Times.Never);
        }
    }
}