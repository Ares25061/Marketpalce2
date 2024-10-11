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
    public class OrderItemServiceTest
    {
        public readonly OrderItemService service;
        private readonly Mock<IOrderItemRepository> repMoq;

        public OrderItemServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IOrderItemRepository>();

            repositoryWrapperMoq.Setup(x => x.OrderItem)
                .Returns(repMoq.Object);

            service = new OrderItemService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectOrderItem()
        {
            return new List<object[]>
            {
                new object[] {new OrderItem { ProductId = 1, Quantity = 0, Price = 0, OrderId = 1,  CreatedBy = 1} },
                new object[] {new OrderItem { ProductId = 1, Quantity = 0, Price = 2, OrderId = 1,  CreatedBy = 1} },
                new object[] {new OrderItem { ProductId = 1, Quantity = 2, Price = 0, OrderId = 1,  CreatedBy = 1} },
            };
        }

        [Fact]
        public async void CreateAsync_NullOrderItem_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<OrderItem>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewOrderItem_ShouldCreateNewOrderItem()
        {
            var example = new OrderItem()
            {
                ProductId = 1,
                Quantity = 2,
                Price = 2,
                OrderId = 1,
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<OrderItem>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectOrderItem))]
        public async Task CreateAsync_NewOrderItem_ShouldNotCreateNewOrderItem(OrderItem model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<OrderItem>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectOrderItemUpdate()
        {
            return new List<object[]>
            {
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 0, Price = 0, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 0, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 0, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.MaxValue, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.MaxValue } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = true, CreatedDate = DateTime.Now, DeletedDate = null, DeletedBy = null } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedDate = DateTime.Now, DeletedBy = null } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedDate = null, DeletedBy = 1 } },
                new object[] {new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 2, OrderId = 1, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, IsDeleted = false, CreatedDate = DateTime.Now, DeletedDate = DateTime.MaxValue, DeletedBy = 1 } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullOrderItem_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<OrderItem>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewOrderItem_ShouldUpdateNewOrderItem()
        {
            var example = new OrderItem()
            {
                OrderItemId = 1,
                ProductId = 1,
                Quantity = 2,
                Price = 2,
                OrderId = 1,
                CreatedBy = 1,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<OrderItem>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectOrderItemUpdate))]
        public async Task UpdateAsync_NewOrderItem_ShouldNotUpdateNewOrderItem(OrderItem model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<OrderItem>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullOrderItem_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<OrderItem, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullOrderItem_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<OrderItem>()), Times.Never);
        }
    }
}