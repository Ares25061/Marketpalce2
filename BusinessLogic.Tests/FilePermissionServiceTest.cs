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
    public class FilePermissionServiceTest
    {
        public readonly FilePermissionService service;
        private readonly Mock<IFilePermissionRepository> repMoq;

        public FilePermissionServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            repMoq = new Mock<IFilePermissionRepository>();

            repositoryWrapperMoq.Setup(x => x.FilePermission)
                .Returns(repMoq.Object);

            service = new FilePermissionService(repositoryWrapperMoq.Object);

        }
        public static IEnumerable<object[]> GetIncorrectFilePermission()
        {
            return new List<object[]>
            {
                new object[] {new FilePermission {FileId = 1, UserId = 1, PermissionLevel = "", CreatedBy = 1} },
            };
        }

        [Fact]
        public async void CreateAsync_NullFilePermission_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<FilePermission>()), Times.Never);
        }


        [Fact]
        public async void CreateAsync_NewFilePermission_ShouldCreateNewFilePermission()
        {
            var example = new FilePermission()
            {
                FileId = 1,
                UserId = 1,
                PermissionLevel = "permissionlevel",
                CreatedBy = 1
            };

            await service.Create(example);

            repMoq.Verify(x => x.Create(It.IsAny<FilePermission>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectFilePermission))]
        public async Task CreateAsync_NewFilePermission_ShouldNotCreateNewFilePermission(FilePermission model)
        {
            var example = model;


            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Create(It.IsAny<FilePermission>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }
        public static IEnumerable<object[]> GetIncorrectFilePermissionUpdate()
        {
            return new List<object[]>
            {
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, } },
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "permissionlevel", IsDeleted = false, CreatedDate = DateTime.MaxValue, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, } },
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "permissionlevel", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.MaxValue, } },
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "permissionlevel", IsDeleted = true, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = null } },
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "permissionlevel", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = null } },
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "permissionlevel", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = null, DeletedDate = DateTime.Now } },
                new object[] {new FilePermission { FilePermissionId = 1, FileId = 1, UserId = 1, PermissionLevel = "permissionlevel", IsDeleted = false, CreatedDate = DateTime.Now, CreatedBy = 1, ModifiedBy = 1, ModifiedDate = DateTime.Now, DeletedBy = 1, DeletedDate = DateTime.MaxValue } },
            };
        }


        [Fact]
        public async void UpdateAsync_NullFilePermission_ShullThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<FilePermission>()), Times.Never);
        }


        [Fact]
        public async void UpdateAsync_NewFilePermission_ShouldUpdateNewFilePermission()
        {
            var example = new FilePermission()
            {
                FilePermissionId = 1,
                FileId = 1,
                UserId = 1,
                PermissionLevel = "permissionlevel",
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now

            };

            await service.Update(example);

            repMoq.Verify(x => x.Update(It.IsAny<FilePermission>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectFilePermissionUpdate))]
        public async Task UpdateAsync_NewFilePermission_ShouldNotUpdateNewFilePermission(FilePermission model)
        {
            var example = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(example));

            Assert.IsType<ArgumentException>(ex);
            repMoq.Verify(x => x.Update(It.IsAny<FilePermission>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void GetByIdAsync_NullFilePermission_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<FilePermission, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_NullFilePermission_ShullThrowArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(-1));

            Assert.IsType<ArgumentNullException>(ex);
            repMoq.Verify(x => x.Delete(It.IsAny<FilePermission>()), Times.Never);
        }
    }
}