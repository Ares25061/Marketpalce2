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
    public class UserFileServiceTest
    {

        public readonly UserFileService service;
        private readonly Mock<IUserFileRepository> repMoq;

        public UserFileServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IUserFileRepository>();

            repositoryWrapperMoq.Setup(x => x.UserFile)
                .Returns(repMoq.Object);

            service = new UserFileService(repositoryWrapperMoq.Object);
        }



        [Fact]
        public async void CreateAsync_NullUserFile_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<UserFile>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewUserFile_ShouldCreateNewUserFile()
        {
            var example = new UserFile()
            {
                UserId = 1,
                FileId = 1
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<UserFile>()), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_NullUserFile_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<UserFile>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewUserFile_ShouldUpdateNewUserFile()
        {
            var example = new UserFile()
            {
                UserId = 1,
                FileId = 1
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<UserFile>()), Times.Once);
        }
        [Fact]
        public async void GetByIdAsync_NullUserFile_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserFile, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullUserFile_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<UserFile>()), Times.Never);
        }

    }
}