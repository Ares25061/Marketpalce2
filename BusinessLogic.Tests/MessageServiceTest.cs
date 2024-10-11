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
    public class MessageServiceTest
    {
        public readonly MessageService service;
        private readonly Mock<IMessageRepository> repMoq;

        public MessageServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IMessageRepository>();

            repositoryWrapperMoq.Setup(x => x.Message)
                .Returns(repMoq.Object);

            service = new MessageService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectMessage()
        {
            return new List<object[]>
            {
                new object[] {new Message {UserId = 1, MessageContent = "", ChatId = 1} },
            };
        }

        [Fact]
        public async void CreateAsync_NullMessage_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Message>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewMessage_ShouldCreateNewMessage()
        {
            var example = new Message()
            {
                UserId = 1,
                MessageContent = "messagecontent",
                ChatId = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Message>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectMessage))]
        public async Task CreateAsync_NewMessage_ShouldNotCreateNewMessage(Message model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Message>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectMessageUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Message { MessageId = 1, UserId = 1, MessageContent = "", ChatId = 1,  IsRead = true, IsDeleted = false, CreatedDate = DateTime.Now } },
                new object[] {new Message { MessageId = 1, UserId = 1, MessageContent = "messagecontent", ChatId = 1,  IsRead = true, IsDeleted = true, CreatedDate = DateTime.Now, DeletedDate = null, DeletedBy=null } },
                new object[] {new Message { MessageId = 1, UserId = 1, MessageContent = "messagecontent", ChatId = 1,  IsRead = true, IsDeleted = false, CreatedDate = DateTime.Now, DeletedDate = DateTime.Now, DeletedBy=null } },
                new object[] {new Message { MessageId = 1, UserId = 1, MessageContent = "messagecontent", ChatId = 1,  IsRead = true, IsDeleted = false, CreatedDate = DateTime.Now, DeletedDate = null, DeletedBy = 1 } },
                new object[] {new Message { MessageId = 1, UserId = 1, MessageContent = "messagecontent", ChatId = 1,  IsRead = true, IsDeleted = false, CreatedDate = DateTime.Now, DeletedDate = DateTime.MaxValue, DeletedBy = 1 } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullMessage_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Message>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewMessage_ShouldUpdateNewMessage()
        {
            var example = new Message()
            {
                MessageId = 1,
                UserId = 1,
                MessageContent = "messagecontent",
                ChatId = 1,
                IsRead = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Message>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectMessageUpdate))]
        public async Task UpdateAsync_NewMessage_ShouldNotUpdateNewMessage(Message model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Message>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullMessage_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Message, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullMessage_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Message>()), Times.Never);
        }
    }
}