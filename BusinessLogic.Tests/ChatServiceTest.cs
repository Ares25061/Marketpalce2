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
    public class ChatServiceTest
    {

        public readonly ChatService service;
        private readonly Mock<IChatRepository> repMoq;

        public ChatServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IChatRepository>();

            repositoryWrapperMoq.Setup(x => x.Chat)
                .Returns(repMoq.Object);

            service = new ChatService(repositoryWrapperMoq.Object);
        }


        public static IEnumerable<object[]> GetIncorrectChat()
        {
            return new List<object[]>
            {
                new object[] {new Chat { Title = "", OwnerId = 1, ModifiedBy= 1 } },
            };
        }


        [Fact]
        public async void CreateAsync_NullChat_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Chat>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewChat_ShouldCreateNewChat()
        {
            var example = new Chat()
            {
                OwnerId = 1,
                Title = "Name",
                ModifiedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<Chat>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectChat))]
        public async Task CreateAsync_NewChat_ShouldNotCreateNewChat(Chat model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<Chat>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        public static IEnumerable<object[]> GetIncorrectChatUpdate()
        {
            return new List<object[]>
            {
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now } },
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "Name", IsDeleted = false, CreatedDate = DateTime.MaxValue, ModifiedDate = DateTime.Now } },
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "Name", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.MaxValue } },
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "Name", IsDeleted = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null} },
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "Name", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null} },
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "Name", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new Chat { ChatId = 1, OwnerId = 1, Title = "Name", IsDeleted = false, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullChat_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Chat>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewChat_ShouldUpdateNewChat()
        {
            var example = new Chat()
            {
                ChatId = 1,
                Title = "Name",
                OwnerId = 1,
                IsDeleted = false,
                ModifiedDate = DateTime.Now,
                ModifiedBy = 1,
                CreatedDate = DateTime.Now,
                DeletedBy = null,
                DeletedDate = null

            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<Chat>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectChatUpdate))]
        public async Task UpdateAsync_NewChat_ShouldNotUpdateNewChat(Chat model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<Chat>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullChat_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Chat, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullChat_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<Chat>()), Times.Never);
        }

    }
}