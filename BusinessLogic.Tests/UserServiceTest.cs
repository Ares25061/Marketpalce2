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
    public class UserServiceTest
    {

        public readonly UserService service;
        private readonly Mock<IUserRepository> repMoq;

        public UserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User)
                .Returns(repMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);
        }


        public static IEnumerable<object[]> GetIncorrectUser()
        {
            return new List<object[]>
            {
                new object[] {new User { Email = "", Password = "", UserName = "", FirstName = "", LastName = "" } },
                new object[] {new User { Email = "email@email.com", Password = "", UserName = "Nick", FirstName = "firstname", LastName = "lastname" } },
                new object[] {new User { Email = "", Password = "12345pass", UserName = "Nick", FirstName = "firstname", LastName = "lastname" } },
                new object[] {new User { Email = "email@email.com", Password = "12345pass", UserName = "", FirstName = "firstname", LastName = "lastname" } },
                new object[] {new User { Email = "email@email.com", Password = "12345pass", UserName = "Nick", FirstName = "", LastName = "lastname" } },
                new object[] {new User { Email = "email@email.com", Password = "12345pass", UserName = "Nick", FirstName = "firstname", LastName = "" } },
            };
        }


        [Fact]
        public async void CreateAsync_NullUser_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewUser_ShouldCreateNewUser()
        {
            var example = new User()
            {
                Email = "email@email.com",
                Password = "12345pass",
                UserName = "Nick",
                FirstName = "firstname",
                LastName = "lastname"
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUser))]
        public async Task CreateAsync_NewUser_ShouldNotCreateNewUser(User model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }


        public static IEnumerable<object[]> UpdateIncorrectUser()
        {
            return new List<object[]>
            {
                new object[] {new User { UserId = 1, Email = "", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false,   CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = true, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.MaxValue, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.MaxValue, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new User { UserId = 1, Email = "email", Password = "Password", UserName = "Nick", FirstName = "firstname", LastName = "lastname", IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullUser_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewUser_ShouldCreateNewUser()
        {
            var example = new User()
            {
                UserId = 1,
                Email = "email@email.com",
                Password = "12345pass",
                UserName = "Nick",
                FirstName = "firstname",
                LastName = "lastname",
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now,
                DeletedBy = null,
                DeletedDate = null
            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(UpdateIncorrectUser))]
        public async Task UpdateAsync_NewChat_ShouldNotUpdateNewChat(User model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<User>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }


        [Fact]
        public async void GetByIdAsync_NullUser_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullUser_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);
        }



    }
}