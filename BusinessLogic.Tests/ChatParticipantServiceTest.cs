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
    public class ChatParticipantServiceTest
    {

        public readonly ChatParticipantService service;
        private readonly Mock<IChatParticipantRepository> repMoq;

        public ChatParticipantServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IChatParticipantRepository>();

            repositoryWrapperMoq.Setup(x => x.ChatParticipant)
                .Returns(repMoq.Object);

            service = new ChatParticipantService(repositoryWrapperMoq.Object);
        }



        [Fact]
        public async void CreateAsync_NullChatParticipant_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<ChatParticipant>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewChatParticipant_ShouldCreateNewChatParticipant()
        {
            var example = new ChatParticipant()
            {
                UserId = 1,
                ChatId = 1
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<ChatParticipant>()), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_NullChatParticipant_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<ChatParticipant>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewChatParticipant_ShouldUpdateNewChatParticipant()
        {
            var example = new ChatParticipant()
            {
                UserId = 1,
                ChatId = 1
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<ChatParticipant>()), Times.Once);
        }
        [Fact]
        public async void GetByIdAsync_NullChatParticipant_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<ChatParticipant, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullChatParticipant_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<ChatParticipant>()), Times.Never);
        }

    }
}