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
    public class NotificationServiceTest
    {
        public readonly NotificationService service;
        private readonly Mock<INotificationRepository> repMoq;

        public NotificationServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<INotificationRepository>();

            repositoryWrapperMoq.Setup(x => x.Notification)
                .Returns(repMoq.Object);

            service = new NotificationService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectNotification()
        {
            return new List<object[]>
            {
                new object[] {new Notification { UserId = 1, Message = "", NotificationType = "", CreatedBy = 1} },
                new object[] {new Notification { UserId = 1, Message = "", NotificationType = "notificationtype", CreatedBy = 1} },
                new object[] {new Notification { UserId = 1, Message = "message", NotificationType = "", CreatedBy = 1} }
            };
        }

        [Fact]
        public async void CreateAsync_NullNotification_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Notification>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewNotification_ShouldCreateNewNotification()
        {
            var example = new Notification()
            {
                UserId = 1,
                Message = "message",
                NotificationType = "notificationtype",
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Notification>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectNotification))]
        public async Task CreateAsync_NewNotification_ShouldNotCreateNewNotification(Notification model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Notification>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectNotificationUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "", NotificationType = "", IsRead=true, CreatedBy = 1,  IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "", NotificationType = "notificationtype", IsRead=true, CreatedBy = 1,  IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "message", NotificationType = "", IsRead=true, CreatedBy = 1,  IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "message", NotificationType = "notificationtype", IsRead=true, CreatedBy = 1,  IsDeleted = true, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "message", NotificationType = "notificationtype", IsRead=true, CreatedBy = 1,  IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "message", NotificationType = "notificationtype", IsRead=true, CreatedBy = 1,  IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new Notification { NotificationId = 1, UserId = 1, Message = "message", NotificationType = "notificationtype", IsRead=true, CreatedBy = 1,  IsDeleted = false, CreatedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullNotification_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Notification>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewNotification_ShouldUpdateNewNotification()
        {
            var example = new Notification()
            {
                NotificationId = 1,
                UserId = 1,
                Message = "message",
                NotificationType = "notificationtype",
                IsRead = true,
                CreatedBy = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Notification>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectNotificationUpdate))]
        public async Task UpdateAsync_NewNotification_ShouldNotUpdateNewNotification(Notification model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Notification>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullNotification_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Notification, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullNotification_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Notification>()), Times.Never);
        }
    }
}